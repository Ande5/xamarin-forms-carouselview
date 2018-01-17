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
            var pagerIndicator = new PagerIndicatorTabs();
            pagerIndicator.SetBinding(PagerIndicatorTabs.ItemsSourceProperty, "Pages");
            pagerIndicator.SetBinding(PagerIndicatorTabs.SelectedItemProperty, "CurrentPage");
            return pagerIndicator;
        }
    }
}
