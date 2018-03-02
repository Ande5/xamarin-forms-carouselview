using Xamarin.Forms;

namespace CarouselView.Controls.Indicator
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T_CasualLayout"></typeparam>
    /// <typeparam name="T_Indicator"></typeparam>
    public class BaseIndicatorConfig
    {
        /// <summary>
        ///     use this method to initial position
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="casualLayout"></param>
        /// <param name="indicator"></param>
        public virtual void InitializePosiotion(RelativeLayout layout, View casualLayout, View indicator)
        {
            layout.Children.Add(casualLayout,
                Constraint.RelativeToParent(parent => parent.X),
                Constraint.RelativeToParent(parent => parent.Y),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToParent(parent => parent.Height));
        }
    }
}