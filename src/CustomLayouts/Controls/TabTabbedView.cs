using CarouselView.Controls.Indicator.Tabs;

namespace CarouselView.Controls
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