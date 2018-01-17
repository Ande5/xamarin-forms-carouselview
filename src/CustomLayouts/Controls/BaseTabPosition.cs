using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T_CasualLayout"></typeparam>
    /// <typeparam name="T_Indicator"></typeparam>
    public class BaseTabPosition
    {
        /// <summary>
        /// use this method to initial position
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="casualLayout"></param>
        /// <param name="indicator"></param>
        public virtual void InitializePosiotion(RelativeLayout layout, View casualLayout, View indicator)
        {
            layout.Children.Add(casualLayout,
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return parent.Y; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToParent((parent) => { return parent.Height; }));
        }

        /// <summary>
        /// Create indicator
        /// </summary>
        /// <param name="casualLayout"></param>
        /// <param name="indicator"></param>
        public virtual View CreateIndicator()
        {
            return null;
        }

        public virtual CarouselLayout CreatePagesCarousel()
        {
            var carousel = new CarouselLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(typeof(HomeView)),
            };
            carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Pages");
            carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentPage", BindingMode.TwoWay);

            return carousel;
        }
    }
}
