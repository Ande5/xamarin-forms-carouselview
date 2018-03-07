using CarouselView.Controls.Indicator.Dots;
using Xamarin.Forms;

namespace CarouselView.Controls
{
    public class DotTabbedView : DotTabbedView<Dot>
    {
    }

    public class DotTabbedView<T_Dot> : CustomTabbedView where T_Dot : Dot, new()
    {
        protected override void InitialComponent()
        {
            base.InitialComponent();
            Indicator = new DotIndicator<T_Dot> {DotSize = 5, DotColor = Color.Black};
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new DotIndicatorConfig();
        }

        protected override void InitialIndicator()
        {
            base.InitialIndicator();

            if (Indicator is DotIndicator<T_Dot> bindableDotIndicator)
            {
                bindableDotIndicator.SetBinding(DotIndicator<T_Dot>.ItemsSourceProperty, "Pages");
                bindableDotIndicator.SetBinding(DotIndicator<T_Dot>.SelectedItemProperty, "CurrentPage");
            }
        }
    }
}