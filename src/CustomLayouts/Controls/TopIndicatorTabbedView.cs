using CustomLayouts.Controls.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls
{
    class TopIndicatorTabbedView : CustomTabbedView
    {
        public TopIndicatorTabbedView()
        {
            Indicator = new PagerIndicatorTabs();
            CasualLayout = new CarouselLayout();
            Generator = new TabIndicatorPosition();
        }
    }
}
