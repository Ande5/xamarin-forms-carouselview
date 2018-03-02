using System.Collections.Generic;
using System.Linq;
using CarouselView.Controls.Interface;
using Xamarin.Forms;

namespace CustomLayoutsDemo.ViewModels
{
    public class SwitcherPageViewModel : BaseViewModel
    {
        private HomeViewModel _currentPage;

        private IEnumerable<HomeViewModel> _pages;

        public SwitcherPageViewModel()
        {
            Pages = new List<HomeViewModel>
            {
                new HomeViewModel {Title = "1", Background = Color.White, ImageSource = "icon.png"},
                new HomeViewModel {Title = "2", Background = Color.Red, ImageSource = "icon.png"},
                new HomeViewModel {Title = "3", Background = Color.Blue, ImageSource = "icon.png"},
                new HomeViewModel {Title = "4", Background = Color.Yellow, ImageSource = "icon.png"},
                new HomeViewModel {Title = "5", Background = Color.White, ImageSource = "icon.png"},
                new HomeViewModel {Title = "6", Background = Color.Red, ImageSource = "icon.png"},
                new HomeViewModel {Title = "7", Background = Color.Blue, ImageSource = "icon.png"},
                new HomeViewModel {Title = "8", Background = Color.Yellow, ImageSource = "icon.png"}
            };

            CurrentPage = Pages.First();
        }

        public IEnumerable<HomeViewModel> Pages
        {
            get => _pages;
            set
            {
                SetObservableProperty(ref _pages, value);
                CurrentPage = Pages.FirstOrDefault();
            }
        }

        public HomeViewModel CurrentPage
        {
            get => _currentPage;
            set => SetObservableProperty(ref _currentPage, value);
        }
    }

    public class HomeViewModel : BaseViewModel, ITabProvider
    {
        public Color Background { get; set; }

        public string Title { get; set; }
        public string ImageSource { get; set; }
    }
}