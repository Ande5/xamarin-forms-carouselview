using System.Collections;
using Xamarin.Forms;

namespace CarouselView.Controls.Indicator.Interface
{
    /// <summary>
    ///     all indicator need to inherit the interface of View
    ///     <scc cref="View" />
    /// </summary>
    public interface Iindicator : IViewController, IVisualElementController, IElementController
    {
        int SelectedIndex { get; }

        IList ItemsSource { get; set; }

        object SelectedItem { get; set; }

        void CreateTabs();

        void ItemsSourceChanging();

        void ItemsSourceChanged();

        void SelectedItemChanged();
    }
}