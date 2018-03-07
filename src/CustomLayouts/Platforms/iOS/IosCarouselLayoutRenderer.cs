using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using CarouselView.Controls.CarouselLayout;
using CarouselView.Platforms.iOS;

[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace CarouselView.Platforms.iOS
{
    public class CarouselLayoutRenderer : ScrollViewRenderer
    {
        UIScrollView _native;

        bool hasNavigation = true;

        public CarouselLayoutRenderer()
        {
            PagingEnabled = true;
            ShowsHorizontalScrollIndicator = false;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;

            _native = (UIScrollView) NativeView;
            _native.Scrolled += NativeScrolled;
            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        void NativeScrolled(object sender, EventArgs e)
        {
            try
            {
                if (!hasNavigation)
                {
                    //if goes in here, it means this page has no navigation bar
                    //make horizontal scroll only
                    //2018/1/18 : xamarin's bug
                    _native.ContentSize = new CGSize(_native.ContentSize.Width, _native.Frame.Size.Height - 25);
                }

                var center = _native.ContentOffset.X + (_native.Bounds.Width / 2);
                ((CarouselLayout) Element).SelectedIndex = ((int) center) / ((int) _native.Bounds.Width);
            }
            catch
            {
                hasNavigation = false;
            }
        }

        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !Dragging)
            {
                ScrollToSelection(false);
            }
        }

        void ScrollToSelection(bool animate)
        {
            if (Element == null) return;

            _native.SetContentOffset(new CoreGraphics.CGPoint
                (_native.Bounds.Width *
                 Math.Max(0, ((CarouselLayout) Element).SelectedIndex),
                    _native.ContentOffset.Y),
                animate);
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            ScrollToSelection(false);
        }
    }
}