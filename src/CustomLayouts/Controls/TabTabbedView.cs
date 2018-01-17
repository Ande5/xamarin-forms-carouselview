using CustomLayouts.Controls.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls
{
    class TabTabbedView : CustomTabbedView
    {
        public TabTabbedView()
        {
            Indicator = new TabIndicator();
            CasualLayout = new CarouselLayout.CarouselLayout();
            Generator = new TabIndicatorConfig();
        }
    }
}
