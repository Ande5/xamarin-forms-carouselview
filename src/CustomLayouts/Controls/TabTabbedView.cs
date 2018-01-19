using CustomLayouts.Controls.Tabs;

namespace CustomLayouts.Controls
{
    public class TabTabbedView : CustomTabbedView
    {
        public TabTabbedView()
        {
            Indicator = new TabIndicator();
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new TabIndicatorConfig();
        }
    }
}