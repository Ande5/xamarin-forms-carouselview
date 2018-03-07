using System;
using CarouselView.Controls.Interface;
using Xamarin.Forms;

namespace CarouselView.Controls.Indicator.Dots
{
    /// <summary>
    ///     Dot
    /// </summary>
    public class Dot : Button, IDot
    {
        public int index { get; set; }

        public Color DotColor { get; set; }
        public double DotSize { get; set; }


        public void Initialize(ITabProvider provider)
        {
            BorderRadius = Convert.ToInt32(DotSize / 2);
            HeightRequest = DotSize;
            WidthRequest = DotSize;
            BackgroundColor = DotColor;
            //Source = provider.ImageSource;
        }

        public virtual void Selected()
        {
            Opacity = 1.0;
        }

        public virtual void UnSelected()
        {
            Opacity = 0.5;
        }
    }
}