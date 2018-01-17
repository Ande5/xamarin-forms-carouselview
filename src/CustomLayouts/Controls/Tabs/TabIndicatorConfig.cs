using Xamarin.Forms;

namespace CustomLayouts.Controls.Tabs
{
    internal class TabIndicatorConfig : BaseIndicatorConfig
    {
        public override void InitializePosiotion(RelativeLayout layout, View casualLayout, View indicator)
        {
            var tabsHeight = 40;
            layout.Children.Add(indicator,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.Constant(tabsHeight)
            );

            layout.Children.Add(casualLayout,
                Constraint.RelativeToParent(parent => { return parent.X; }),
                Constraint.RelativeToParent(parent => { return parent.Y + tabsHeight; }),
                Constraint.RelativeToParent(parent => { return parent.Width; }),
                Constraint.RelativeToParent(parent => { return parent.Height - tabsHeight; })
            );
        }
    }
}