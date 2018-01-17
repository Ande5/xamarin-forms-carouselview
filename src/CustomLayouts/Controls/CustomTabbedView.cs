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
    public class CustomTabbedView : RelativeLayout 
    {
        private CarouselLayout _CarouselLayout;
        public CarouselLayout CasualLayout
        {
            get => _CarouselLayout;
            set
            {
                _CarouselLayout = value;
                InitialView();
            }
        }

        private BaseIndicator _baseIndicator;
        public BaseIndicator Indicator
        {
            get => _baseIndicator;
            set
            {
                _baseIndicator = value;
                InitialView();
            }
        }

        private BaseTabPosition _baseTabPosition;
        public BaseTabPosition Generator
        {
            get => _baseTabPosition;
            set
            {
                _baseTabPosition = value;
                InitialView();
            }
        }

        public CustomTabbedView()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }

        void InitialView()
        {
            if (CasualLayout != null && Indicator != null && Generator != null)
            {
                try
                {
                    CasualLayout.SetBinding(CarouselLayout.ItemsSourceProperty, "Pages");
                    CasualLayout.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentPage", BindingMode.TwoWay);

                    //目前先暫時這樣包裝
                    if (Indicator is PagerIndicatorTabs bindableIndicator)
                    {
                        bindableIndicator.SetBinding(PagerIndicatorTabs.ItemsSourceProperty, "Pages");
                        bindableIndicator.SetBinding(PagerIndicatorTabs.SelectedItemProperty, "CurrentPage");
                        //bindableIndicator.SetBinding(CustomLayouts.Indicator.ItemsSourceProperty, "Pages");
                        //bindableIndicator.SetBinding(CustomLayouts.Indicator.SelectedItemProperty, "CurrentPage");
                    }
                    else if (Indicator is PagerIndicatorDots bindableDotIndicator)
                    {
                        bindableDotIndicator.SetBinding(PagerIndicatorDots.ItemsSourceProperty, "Pages");
                        bindableDotIndicator.SetBinding(PagerIndicatorDots.SelectedItemProperty, "CurrentPage");
                    }

                    //generate position
                    Generator.InitializePosiotion(this, CasualLayout, Indicator as View);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
