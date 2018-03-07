using CarouselView.Controls.Indicator.Dots;
using Xamarin.Forms;

namespace CarouselView.Controls
{
    public class DotTabbedView : CustomTabbedView
    {
        public DotTabbedView()
        {
            Indicator = new DotIndicator<Dot> {DotSize = 5, DotColor = Color.Black};
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new DotIndicatorConfig();
        }

        protected override void InitialIndicator()
        {
            base.InitialIndicator();

            if (Indicator is DotIndicator<Dot> bindableDotIndicator)
            {
                bindableDotIndicator.SetBinding(DotIndicator<Dot>.ItemsSourceProperty, "Pages");
                bindableDotIndicator.SetBinding(DotIndicator<Dot>.SelectedItemProperty, "CurrentPage");
            }
        }
    }
}