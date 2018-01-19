using Xamarin.Forms;

namespace CustomLayoutsDemo
{
    public class HomeView : ContentView
    {
        public HomeView()
        {
            BackgroundColor = Color.White;

            var label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };

            label.SetBinding(Label.TextProperty, "Title");
            this.SetBinding(BackgroundColorProperty, "Background");

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    label
                }
            };
        }
    }
}