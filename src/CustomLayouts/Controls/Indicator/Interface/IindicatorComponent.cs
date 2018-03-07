using CarouselView.Controls.Interface;

namespace CarouselView.Controls.Indicator.Interface
{
    /// <summary>
    ///     indicator component
    /// </summary>
    public interface IindicatorComponent
    {
        int index { get; set; }

        void Initialize(ITabProvider provider);

        void Selected();

        void UnSelected();
    }
}