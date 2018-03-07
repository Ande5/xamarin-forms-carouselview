using CarouselView.Controls.Indicator.Tabs;
using Xamarin.Forms;

namespace CarouselView.Controls
{
    public class TabTabbedView : CustomTabbedView
    {
        public TabTabbedView()
        {
            Indicator = new TabIndicator<Tab>();
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new TabIndicatorConfig();
        }

        protected override void InitialIndicator()
        {
            base.InitialIndicator();
            if (Indicator is TabIndicator<Tab> bindableIndicator)
            {
                bindableIndicator.SetBinding(TabIndicator<Tab>.ItemsSourceProperty, "Pages");
                bindableIndicator.SetBinding(TabIndicator<Tab>.SelectedItemProperty, "CurrentPage");
                //bindableIndicator.SetBinding(CustomLayouts.Indicator.ItemsSourceProperty, "Pages");
                //bindableIndicator.SetBinding(CustomLayouts.Indicator.SelectedItemProperty, "CurrentPage");
            }
        }
    }
}