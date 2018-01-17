using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts.Controls.Dots
{
    public class DotIndicatorConfig : BaseIndicatorConfig
    {
        public override void InitializePosiotion(RelativeLayout layout, View casualLayout, View indicator)
        {
            layout.Children.Add(casualLayout,
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return parent.Y; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToParent((parent) => { return parent.Height / 2; })
                    );

            layout.Children.Add(indicator,
                Constraint.Constant(0),
                Constraint.RelativeToView(casualLayout,
                    (parent, sibling) => { return sibling.Height - 18; }),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.Constant(18)
            );
        }
    }
}
