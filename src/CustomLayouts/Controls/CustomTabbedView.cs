using System;
using CarouselView.Controls.Indicator;
using CarouselView.Controls.Indicator.Interface;
using Xamarin.Forms;

namespace CarouselView.Controls
{
    /// <summary>
    ///     CustomTabbedView need assigh :
    ///     1. CasualLayout (use to show multiple contectView)
    ///     2. Tab (use to show tab)
    ///     3. tab position (use to assign position)
    /// </summary>
    public class CustomTabbedView : RelativeLayout
    {
        private Iindicator _baseIndicator;

        private BaseIndicatorConfig _baseTabPosition;
        private CarouselLayout.CarouselLayout _carouselLayout;

        public CustomTabbedView()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            InitialComponent();
        }

        public CarouselLayout.CarouselLayout CasualLayout
        {
            get => _carouselLayout;
            set
            {
                _carouselLayout = value;
                InitialView();
            }
        }

        public Iindicator Indicator
        {
            get => _baseIndicator;
            set
            {
                _baseIndicator = value;
                InitialView();
            }
        }

        public BaseIndicatorConfig Generator
        {
            get => _baseTabPosition;
            set
            {
                _baseTabPosition = value;
                InitialView();
            }
        }

        protected virtual void InitialComponent()
        {
        }

        protected virtual void InitialView()
        {
            if (CasualLayout != null && Indicator != null && Generator != null)
                try
                {
                    //initial page
                    CasualLayout.SetBinding(CarouselLayout.CarouselLayout.ItemsSourceProperty, "Pages");
                    CasualLayout.SetBinding(CarouselLayout.CarouselLayout.SelectedItemProperty, "CurrentPage",
                        BindingMode.TwoWay);

                    //initial indicator
                    InitialIndicator();

                    //generate position
                    Generator.InitializePosiotion(this, CasualLayout, Indicator as View);
                }
                catch (Exception ex)
                {
                }
        }

        protected virtual void InitialIndicator()
        {
        }
    }
}