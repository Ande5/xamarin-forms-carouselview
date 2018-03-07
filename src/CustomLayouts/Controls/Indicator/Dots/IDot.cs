using CarouselView.Controls.Indicator.Interface;
using Xamarin.Forms;

namespace CarouselView.Controls.Indicator.Dots
{
    /// <summary>
    ///     Dot
    /// </summary>
    public interface IDot : IindicatorComponent
    {
        Color DotColor { get; set; }

        double DotSize { get; set; }
    }
}