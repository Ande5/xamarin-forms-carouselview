using Xamarin.Forms;

namespace CarouselView.Controls.Indicator.Tabs
{
    public class TabIndicatorConfig : BaseIndicatorConfig
    {
        public override void InitializePosiotion(RelativeLayout layout, View casualLayout, View indicator)
        {
            var tabsHeight = indicator.HeightRequest;
            layout.Children.Add(indicator,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.Constant(tabsHeight)
            );

            layout.Children.Add(casualLayout,
                Constraint.RelativeToParent(parent => parent.X),
                Constraint.RelativeToParent(parent => parent.Y + tabsHeight),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToParent(parent => parent.Height - tabsHeight)
            );
        }
    }
}