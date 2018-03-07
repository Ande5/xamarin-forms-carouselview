using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using Android.Animation;
using Android.Content;
using Android.Widget;
using CarouselView.Controls.Indicator.Tabs;
using CarouselView.Platforms.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TabIndicatorScrollView), typeof(TabIndicatorRenderer))]

namespace CarouselView.Platforms.Android
{
    public class TabIndicatorRenderer : ScrollViewRenderer
    {
        private HorizontalScrollView _scrollView;


        public TabIndicatorRenderer(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            e.NewElement.PropertyChanged += ElementPropertyChanged;
        }

        private void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                _scrollView = (HorizontalScrollView) typeof(ScrollViewRenderer)
                    .GetField("_hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);

                _scrollView.HorizontalScrollBarEnabled = false;

                if (Element != null && Element is IScrollViewController controller)
                    controller.ScrollToRequested += OnScrollToRequested;
            }
        }

        private async void OnScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
            // 99.99% of the time simply queuing to the end of the execution queue should handle this case.
            // However it is possible to end a layout cycle and STILL be layout requested. We want to
            // back off until all are done, even if they trigger layout storms over and over. So we back off
            // for 10ms tops then move on.
            var cycle = 0;
            while (IsLayoutRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1));
                cycle++;

                if (cycle >= 10)
                    break;
            }

            var context = Context;

            var x = (int) context.ToPixels(e.ScrollX);
            var y = (int) context.ToPixels(e.ScrollY);

            ScrollTo(x, y, 500);
        }


        public void ScrollTo(int targetX, int targetY, int duration = 500)
        {
            //https://stackoverflow.com/questions/8642677/reduce-speed-of-smooth-scroll-in-scroll-view/33013806
            //TODO : android animation is weird 

            var animator = ObjectAnimator.OfInt(target: _scrollView, propertyName: "scrollX", values: targetX);
            animator.SetDuration(duration);
            animator.Start();
        }
    }
}