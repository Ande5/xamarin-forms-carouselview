using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarouselView.Controls.Interface;
using Xamarin.Forms;

namespace CarouselView.Controls.CarouselLayout
{
    public class CarouselLayout : ScrollView
    {
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(CarouselLayout),
                -1,
                BindingMode.TwoWay,
                propertyChanged: async (bindable, oldValue, newValue) =>
                {
                    await ((CarouselLayout) bindable).UpdateSelectedItem();
                }
            );

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(CarouselLayout),
                null,
                propertyChanging: (bindableObject, oldValue, newValue) =>
                {
                    ((CarouselLayout) bindableObject).ItemsSourceChanging();
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((CarouselLayout) bindableObject).ItemsSourceChanged();
                }
            );

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(CarouselLayout),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselLayout) bindable).UpdateSelectedIndex();
                }
            );

        private readonly StackLayout _stack;

        private bool _layingOutChildren;

        private int _selectedIndex;

        public CarouselLayout()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            Orientation = ScrollOrientation.Horizontal;

            _stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            Content = _stack;
        }

        public EventHandler<int> OnSelectdPageChanged { get; set; }

        public IList<View> Children => _stack.Children;

        public int SelectedIndex
        {
            get => (int) GetValue(SelectedIndexProperty);
            set
            {
                if (SelectedIndex != value)
                {
                    //set index
                    SetValue(SelectedIndexProperty, value);

                    //notify selected page changed
                    var page = SelectedIndex > -1 ? Children[SelectedIndex] : null;
                    if (page is IPageProvider pageProvider)
                    {
                        pageProvider.OnPageSelected();
                        OnSelectdPageChanged?.Invoke(SelectedIndex, SelectedIndex);
                    }
                }
            }
        }

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate { get; set; }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if (_layingOutChildren) return;

            _layingOutChildren = true;
            foreach (var child in Children) child.WidthRequest = width;
            _layingOutChildren = false;
        }

        private async Task UpdateSelectedItem()
        {
            await Task.Delay(300);
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
        }

        private void ItemsSourceChanging()
        {
            if (ItemsSource == null) return;
            _selectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        private void ItemsSourceChanged()
        {
            _stack.Children.Clear();
            foreach (var item in ItemsSource)
            {
                View view = null;

                if (ItemTemplate is DataTemplateSelector dataTemplateSelector)
                    view = (View) dataTemplateSelector.SelectTemplate(item, this).CreateContent();
                else
                    view = (View) ItemTemplate.CreateContent();

                var bindableObject = view as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = item;
                _stack.Children.Add(view);
            }

            if (_selectedIndex >= 0)
                SelectedIndex = _selectedIndex;
        }

        private void UpdateSelectedIndex()
        {
            if (SelectedItem == BindingContext) return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);
        }
    }
}