using CarouselView.Controls.Interface;
using Xamarin.Forms;

namespace CarouselView.Controls.Indicator.Tabs
{
    /// <summary>
    ///     Tab
    /// </summary>
    public class Tab : StackLayout, ITab
    {
        protected Label TextLabel { get; set; }

        protected BoxView BottomView { get; set; }

        /// <summary>
        ///     Index
        /// </summary>
        public int index { get; set; }

        public virtual void Initialize(ITabProvider provider)
        {
            Orientation = StackOrientation.Vertical;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            WidthRequest = 94;
            HeightRequest = 44;
            Spacing = 0;

            Children.Add(TextLabel = new Label
            {
                Text = provider?.Title,
                FontSize = 16,
                HeightRequest = 42,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center
            });

            Children.Add(BottomView = new BoxView
            {
                HeightRequest = 2,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            });
        }

        public virtual void Selected()
        {
            //tab.Opacity = 1.0;
            TextLabel.TextColor = Color.FromHex("#954DB3");
            BottomView.BackgroundColor = Color.FromHex("#954DB3");
        }

        public virtual void UnSelected()
        {
            // tab.Opacity = 0.5;
            TextLabel.TextColor = Color.FromHex("#000000");
            BottomView.BackgroundColor = Color.FromHex("#FFFFFF");
        }
    }
}