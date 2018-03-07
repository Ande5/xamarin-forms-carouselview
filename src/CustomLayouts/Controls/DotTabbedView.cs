using CarouselView.Controls.Indicator.Dots;
using Xamarin.Forms;

namespace CarouselView.Controls
{
    public class DotTabbedView : CustomTabbedView
    {
        public DotTabbedView()
        {
            Indicator = new DotIndicator {DotSize = 5, DotColor = Color.Black};
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new DotIndicatorConfig();
        }

        protected override void InitialIndicator()
        {
            base.InitialIndicator();

            if (Indicator is DotIndicator bindableDotIndicator)
            {
                bindableDotIndicator.SetBinding(DotIndicator.ItemsSourceProperty, "Pages");
                bindableDotIndicator.SetBinding(DotIndicator.SelectedItemProperty, "CurrentPage");
            }
        }
    }
}