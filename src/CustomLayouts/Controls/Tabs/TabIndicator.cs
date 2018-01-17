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
            GridContainer.RowDefinitions.Add(new RowDefinition {Height = 40});
            Content = GridContainer;
            Orientation = ScrollOrientation.Horizontal;

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
                var tab = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(7),
                    WidthRequest = 70
                };

                if (item is ITabProvider homeViewModel)
                    tab.Children.Add(new Label
                    {
                        Text = homeViewModel.Title,
                        FontSize = 11,
                        TextColor = Color.Black
                    });

                /*
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        tab.Children.Add(new Image { Source = "pin.png", HeightRequest = 20 });
                        tab.Children.Add(new Label { Text = "Tab " + (index + 1), FontSize = 11 });
                        break;

                    case Device.Android:
                        tab.Children.Add(new Image { Source = "pin.png", HeightRequest = 25 });
                        break;
                }
                */

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
            var pagerIndicators = GridContainer.Children.Cast<StackLayout>().ToList();

            foreach (var pi in pagerIndicators)
                UnselectTab(pi);

            if (selectedIndex > -1)
                SelectTab(pagerIndicators[selectedIndex]);

            //取得所有欄位的寬度
            var listWidth = pagerIndicators.Select(x => x.Width);

            //目前要移動的位置
            var targetPoition = listWidth.Take(selectedIndex == 0 ? 0 : selectedIndex - 1).Sum();

            //如果在可以捲動的範圍
            if (targetPoition < GridContainer.Width - Width)
                await ScrollToAsync(targetPoition, 0, true);
        }

        private static void UnselectTab(StackLayout tab)
        {
            tab.Opacity = 0.5;
        }

        private static void SelectTab(StackLayout tab)
        {
            tab.Opacity = 1.0;
        }
    }
}