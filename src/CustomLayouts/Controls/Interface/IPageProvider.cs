namespace CarouselView.Controls.Interface
{
    /// <summary>
    ///     if page inherit this ,will notify this page
    ///     for example , swipe to this page and nofify this page to load data from api
    /// </summary>
    public interface IPageProvider
    {
        /// <summary>
        ///     notified is this page
        /// </summary>
        void OnPageSelected();
    }
}