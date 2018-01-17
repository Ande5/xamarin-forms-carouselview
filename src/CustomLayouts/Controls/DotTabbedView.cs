using CustomLayouts.Controls.Dots;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls
{
    public class DotTabbedView : CustomTabbedView
    {
        public DotTabbedView()
        {
            Indicator = new PagerIndicatorDots() { DotSize = 5, DotColor = Color.Black };
            CasualLayout = new CarouselLayout();
            Generator = new DotIndicatorPosition();
        }
    }
}
