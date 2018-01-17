using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls.Tabs
{
    class TabIndicatorPosition : BaseTabPosition
    {
        public override void InitializePosiotion(RelativeLayout layout, View casualLayout, View indicator)
        {
            var tabsHeight = 25;
            layout.Children.Add(indicator,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.Constant(tabsHeight)
            );

            layout.Children.Add(casualLayout,
                Constraint.RelativeToParent((parent) => { return parent.X; }),
                Constraint.RelativeToParent((parent) => { return parent.Y + tabsHeight; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height - tabsHeight; })
            );
        }

        public override View CreateIndicator()
        {
            var tabscrollView = new ScrollView();

            var pagerIndicator = new PagerIndicatorTabs();
            pagerIndicator.WidthRequest = 500;

            pagerIndicator.RowDefinitions.Add(new RowDefinition() { Height = 25 });
            pagerIndicator.SetBinding(PagerIndicatorTabs.ItemsSourceProperty, "Pages");
            pagerIndicator.SetBinding(PagerIndicatorTabs.SelectedItemProperty, "CurrentPage");

            tabscrollView.Content = pagerIndicator;

            tabscrollView.ScrollToAsync(100, 0, true);

            tabscrollView.Orientation = ScrollOrientation.Horizontal;
            tabscrollView.WidthRequest = 250;

            return tabscrollView;
        }
    }
}
