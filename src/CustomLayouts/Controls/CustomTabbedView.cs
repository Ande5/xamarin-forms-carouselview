using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls
{
    /// <summary>
    /// CustomTabbedView need assigh : 
    /// 1. CasualLayout (use to show multiple contectView)
    /// 2. Tab (use to show tab)
    /// 3. tab position (use to assign position)
    /// </summary>
    /// <typeparam name="T_CasualLayout"></typeparam>
    /// <typeparam name="T_Indicator"></typeparam>
    /// <typeparam name="T_Position"></typeparam>
    class CustomTabbedView<T_CasualLayout,T_Indicator,T_Position> : RelativeLayout 
        where T_CasualLayout : CarouselLayout where T_Indicator : BaseIndicator where T_Position : BaseTabPosition ,new()
    {
        T_CasualLayout CasualLayout { get; set; }
        T_Indicator Indicator { get; set; }
        T_Position Generator { get; set; }
        public CustomTabbedView()
        {
            Generator = new T_Position();

            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            //pages
            var pagesCarousel = Generator.CreatePagesCarousel();

            //indicator
            var dots = Generator.CreateIndicator();

            //generate position
            Generator.InitializePosiotion(this, pagesCarousel, dots);
        }
    }
}
