using System;
using System.Collections;
using System.Linq;
using CustomLayouts.Controls.Interface;
using Xamarin.Forms;

namespace CustomLayouts.Controls.Tabs
{
    /// <summary>
    ///     tab style indicator
    /// </summary>
    public class TabIndicator : ScrollView, BaseIndicator
    {
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(TabIndicator),
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    ((TabIndicator) bindable).ItemsSourceChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) => { ((TabIndicator) bindable).ItemsSourceChanged(); }
            );

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(TabIndicator),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) => { ((TabIndicator) bindable).SelectedItemChanged(); }
            );

        public TabIndicator()
        {
            //GridContainer.WidthRequest = 500;
            GridContainer.RowDefinitions.Add(new RowDefinition {Height = 44});
            GridContainer.VerticalOptions = LayoutOptions.FillAndExpand;


            var layout = new StackLayout
            {
                Spacing = 0
            };
            layout.Children.Add(GridContainer);
            layout.Children.Add(new BoxView
            {
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#DDDDDD"),
                VerticalOptions = LayoutOptions.End
            });
            Content = layout;
            Orientation = ScrollOrientation.Horizontal;

            HeightRequest = 45;

            /*
            var assembly = typeof(PagerIndicatorTabs).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            */
        }

        public Grid GridContainer { get; set; } = new Grid
        {
            //HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Color.White
        };

        public int SelectedIndex { get; private set; }

        public void CreateTabs()
        {
            if (GridContainer.Children != null && GridContainer.Children.Count > 0) GridContainer.Children.Clear();

            foreach (var item in ItemsSource)
            {
                var index = GridContainer.Children.Count;
                var tab = new Tab(item as ITabProvider);

                var tgr = new TapGestureRecognizer();
                tgr.Command = new Command(() => { SelectedItem = ItemsSource[index]; });
                tab.GestureRecognizers.Add(tgr);
                GridContainer.Children.Add(tab, index, 0);
            }
        }

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public void ItemsSourceChanging()
        {
            if (ItemsSource != null)
                SelectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        public void ItemsSourceChanged()
        {
            if (ItemsSource == null) return;

            GridContainer.ColumnDefinitions.Clear();
            foreach (var item in ItemsSource)
                GridContainer.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

            CreateTabs();
        }

        public async void SelectedItemChanged()
        {
            var selectedIndex = ItemsSource.IndexOf(SelectedItem);
            var pagerIndicators = GridContainer.Children.Cast<Tab>().ToList();

            foreach (var pi in pagerIndicators)
                UnselectTab(pi);

            if (selectedIndex > -1)
                SelectTab(pagerIndicators[selectedIndex]);

            //取得所有欄位的寬度
            var listWidth = pagerIndicators.Select(x => x.Width);

            //目前要移動的位置
            var targetPoition = listWidth.Take(selectedIndex == 0 ? 0 : selectedIndex - 1).Sum();

            //捲動
            targetPoition = Math.Min(targetPoition, GridContainer.Width - Width);
            await ScrollToAsync(targetPoition, 0, true);
        }

        private static void UnselectTab(Tab tab)
        {
            tab.UnselectTab();
        }

        private static void SelectTab(Tab tab)
        {
            tab.SelectTab();
        }
    }

    /// <summary>
    ///     Tab
    /// </summary>
    public class Tab : StackLayout
    {
        public Tab(ITabProvider provider)
        {
            Orientation = StackOrientation.Vertical;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            WidthRequest = 94;
            HeightRequest = 44;
            Spacing = 0;


            Children.Add(TextLabel = new Label
            {
                Text = provider?.Title,
                FontSize = 16,
                HeightRequest = 42,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center
            });

            Children.Add(BottomView = new BoxView
            {
                HeightRequest = 2,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            });
        }

        public Label TextLabel { get; }

        public BoxView BottomView { get; }

        public void SelectTab()
        {
            //tab.Opacity = 1.0;
            TextLabel.TextColor = Color.FromHex("#954DB3");
            BottomView.BackgroundColor = Color.FromHex("#954DB3");
        }

        public void UnselectTab()
        {
            // tab.Opacity = 0.5;
            TextLabel.TextColor = Color.FromHex("#000000");
            BottomView.BackgroundColor = Color.FromHex("#FFFFFF");
        }
    }
}