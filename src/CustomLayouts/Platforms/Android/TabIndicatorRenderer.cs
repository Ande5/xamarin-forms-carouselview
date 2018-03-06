using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using CarouselView.Controls.CarouselLayout;
using CarouselView.Controls.Indicator.Tabs;
using CarouselView.Platforms.Android;
using Java.Lang;
using Xamarin.Forms.Platform.Android;
using Point = System.Drawing.Point;
using View = Android.Views.View;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TabIndicator), typeof(TabIndicatorRenderer))]
namespace CarouselView.Platforms.Android
{
    public class TabIndicatorRenderer : ScrollViewRenderer
    {
        int _deltaX;

        bool _initialized = false;
        bool _motionDown;
        int _prevScrollX;
        HorizontalScrollView _scrollView;


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

        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                _scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
                    .GetField("_hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);

                _scrollView.HorizontalScrollBarEnabled = false;

                //_scrollView.Touch += HScrollViewTouch;

                if (Element != null && Element is Xamarin.Forms.IScrollViewController controller)
                {
                    controller.ScrollToRequested += OnScrollToRequested;
                }
            }
        }

        /*
        void HScrollViewTouch(object sender, View.TouchEventArgs e)
        {
            e.Handled = false;

            switch (e.Event.Action)
            {
                case MotionEventActions.Move:
                    _deltaXResetTimer.Stop();
                    _deltaX = _scrollView.ScrollX - _prevScrollX;
                    _prevScrollX = _scrollView.ScrollX;

                    UpdateSelectedIndex();

                    _deltaXResetTimer.Start();
                    break;
                case MotionEventActions.Down:
                    _motionDown = true;
                    _scrollStopTimer.Stop();
                    break;
                case MotionEventActions.Up:
                    _motionDown = false;
                    SnapScroll();
                    _scrollStopTimer.Start();
                    break;
            }
        }
        */
        /*
        void UpdateSelectedIndex()
        {
            var center = _scrollView.ScrollX + (_scrollView.Width / 2);
            var carouselLayout = (TabIndicator)this.Element;
            //carouselLayout.SelectedIndex = (center / _scrollView.Width);
        }
        */

        /*
        void SnapScroll()
        {
            var roughIndex = (float)_scrollView.ScrollX / _scrollView.Width;

            var targetIndex =
                _deltaX < 0
                    ? Math.Floor(roughIndex)
                    : _deltaX > 0
                        ? Math.Ceil(roughIndex)
                        : Math.Round(roughIndex);

            ScrollToIndex((int)targetIndex);
        }
        */

        /*
        void ScrollToIndex(int targetIndex)
        {
            var targetX = targetIndex * _scrollView.Width;
            _scrollView.Post(new Runnable(() => { _scrollView.SmoothScrollTo(targetX, 0); }));
        }
        */

        /*
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            if (_initialized) return;
            _initialized = true;
            var carouselLayout = (TabIndicator)this.Element;
            _scrollView.ScrollTo(carouselLayout.SelectedIndex * Width, 0);
        }
        */

        /*
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            if (_initialized && (w != oldw))
            {
                _initialized = false;
            }
            base.OnSizeChanged(w, h, oldw, oldh);
        }
        */

        
        async void OnScrollToRequested(object sender, Xamarin.Forms.ScrollToRequestedEventArgs e)
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

            /*
            var itemPosition = Controller.GetScrollPositionForElement(Element, e.Position);

            int x = (int)context.ToPixels(itemPosition.X);
            int y = (int)context.ToPixels(itemPosition.Y);
            */

            int x = (int)context.ToPixels(e.ScrollX);
            int y = (int)context.ToPixels(e.ScrollY);

            ScrollTo(x, y);

            /*
            e.

            
            var x = (int)context.ToPixels(e.ScrollX);
            var y = (int)context.ToPixels(e.ScrollY);
            int currentX = _view.Orientation == ScrollOrientation.Horizontal || _view.Orientation == ScrollOrientation.Both ? _hScrollView.ScrollX : ScrollX;
            int currentY = _view.Orientation == ScrollOrientation.Vertical || _view.Orientation == ScrollOrientation.Both ? ScrollY : _hScrollView.ScrollY;
            if (e.Mode == ScrollToMode.Element)
            {
                var itemPosition = Controller.GetScrollPositionForElement(e.Element as VisualElement, e.Position);

                x = (int)context.ToPixels(itemPosition.X);
                y = (int)context.ToPixels(itemPosition.Y);
            }
            if (e.ShouldAnimate)
            {
                ValueAnimator animator = ValueAnimator.OfFloat(0f, 1f);
                animator.SetDuration(1000);
                animator.Update += (o, animatorUpdateEventArgs) =>
                {
                    var v = (double)animatorUpdateEventArgs.Animation.AnimatedValue;
                    int distX = GetDistance(currentX, x, v);
                    int distY = GetDistance(currentY, y, v);

                    if (_view == null)
                    {
                        // This is probably happening because the page with this Scroll View
                        // was popped off the stack during animation
                        animator.Cancel();
                        return;
                    }

                    switch (_view.Orientation)
                    {
                        case ScrollOrientation.Horizontal:
                            _hScrollView.ScrollTo(distX, distY);
                            break;
                        case ScrollOrientation.Vertical:
                            ScrollTo(distX, distY);
                            break;
                        default:
                            _hScrollView.ScrollTo(distX, distY);
                            ScrollTo(distX, distY);
                            break;
                    }
                };
                animator.AnimationEnd += delegate
                {
                    if (Controller == null)
                        return;
                    Controller.SendScrollFinished();
                };

                animator.Start();
            }
            else
            {
                switch (_view.Orientation)
                {
                    case ScrollOrientation.Horizontal:
                        _hScrollView.ScrollTo(x, y);
                        break;
                    case ScrollOrientation.Vertical:
                        ScrollTo(x, y);
                        break;
                    default:
                        _hScrollView.ScrollTo(x, y);
                        ScrollTo(x, y);
                        break;
                }
                Controller.SendScrollFinished();
            }
            */
        }
        

        public new void ScrollTo(int targetX, int targetY)
        {
            int duration = 500;
            ObjectAnimator animator = ObjectAnimator.OfInt(target: _scrollView, propertyName: "scrollX", values: targetX);
            animator.SetDuration(duration);
            animator.Start();
        }

        /*
        public void ScrollTo(int targetXScroll,long duration = 500)
        {
            ObjectAnimator animator = ObjectAnimator.OfInt(_scrollView, "scrollX", targetXScroll);
            animator.SetDuration(duration);
            animator.Start();
        }
        */

    }
}
