using System.Collections;
using System.Linq;
using CarouselView.Controls.Indicator.Interface;
using CarouselView.Controls.Interface;
using Xamarin.Forms;

namespace CarouselView.Controls.Indicator.Dots
{
    /// <summary>
    ///     dot style indicator
    /// </summary>
    public class DotIndicator<T_Tab> : StackLayout, Iindicator where T_Tab : View, IDot, new()
    {
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(DotIndicator<T_Tab>),
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    ((DotIndicator<T_Tab>) bindable).ItemsSourceChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((DotIndicator<T_Tab>) bindable).ItemsSourceChanged();
                }
            );

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(DotIndicator<T_Tab>),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((DotIndicator<T_Tab>) bindable).SelectedItemChanged();
                }
            );

        public DotIndicator()
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.Center;
            Orientation = StackOrientation.Horizontal;
            DotColor = Color.Black;
        }

        public Color DotColor { get; set; }
        public double DotSize { get; set; }

        public int SelectedIndex { get; private set; }

        public void CreateTabs()
        {
            foreach (var item in ItemsSource)
            {
                var index = Children.Count;
                var tab = item as ITabProvider;
                var dot = new T_Tab();
                dot.index = index;
                dot.DotColor = DotColor;
                dot.DotSize = DotSize;
                dot.Initialize(tab);
                Children.Add(dot);
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

            CreateTabs();
        }

        public void SelectedItemChanged()
        {
            var selectedIndex = ItemsSource.IndexOf(SelectedItem);
            var pagerIndicators = Children.Cast<IDot>().ToList();

            foreach (var pi in pagerIndicators)
                UnselectDot(pi);

            if (selectedIndex > -1)
                SelectDot(pagerIndicators[selectedIndex]);
        }

        private static void UnselectDot(IDot dot)
        {
            dot.UnSelected();
        }

        private static void SelectDot(IDot dot)
        {
            dot.Selected();
        }
    }
}