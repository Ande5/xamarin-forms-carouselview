using CarouselView.Controls.Indicator.Tabs;
using Xamarin.Forms;

namespace CarouselView.Controls
{
    public class TabTabbedView : TabTabbedView<Tab>
    {
    }

    public class TabTabbedView<T_Tab> : CustomTabbedView where T_Tab : Tab, new()
    {
        protected override void InitialComponent()
        {
            base.InitialComponent();
            Indicator = new TabIndicator<T_Tab>();
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new TabIndicatorConfig();
        }

        protected override void InitialIndicator()
        {
            base.InitialIndicator();
            if (Indicator is TabIndicator<T_Tab> bindableIndicator)
            {
                bindableIndicator.SetBinding(TabIndicator<T_Tab>.ItemsSourceProperty, "Pages");
                bindableIndicator.SetBinding(TabIndicator<T_Tab>.SelectedItemProperty, "CurrentPage");
                //bindableIndicator.SetBinding(CustomLayouts.Indicator.ItemsSourceProperty, "Pages");
                //bindableIndicator.SetBinding(CustomLayouts.Indicator.SelectedItemProperty, "CurrentPage");
            }
        }
    }
}