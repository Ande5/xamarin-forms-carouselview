using System;
using CarouselView.Controls;
using CustomLayoutsDemo.ViewModels;
using Xamarin.Forms;

namespace CustomLayoutsDemo
{
    public enum IndicatorStyleEnum
    {
        None,
        Dots,
        Tabs
    }

    public class HomePage : ContentPage
    {
        private readonly SwitcherPageViewModel viewModel;
        private IndicatorStyleEnum _indicatorStyle;
        private RelativeLayout relativeLayout;

        public HomePage(IndicatorStyleEnum indicatorStyle)
        {
            try
            {
                viewModel = new SwitcherPageViewModel();
                BindingContext = viewModel;
                Title = _indicatorStyle.ToString();

                CustomTabbedView tabbedView = null;

                switch (indicatorStyle)
                {
                    case IndicatorStyleEnum.Dots:
                        tabbedView = new DotTabbedView();
                        break;
                    case IndicatorStyleEnum.Tabs:
                        tabbedView = new TabTabbedView();
                        break;
                    case IndicatorStyleEnum.None:
                        tabbedView = new TabTabbedView();
                        break;
                }

                tabbedView.CasualLayout.ItemTemplate = new DataTemplate(typeof(HomeView));

                Content = tabbedView;
            }
            catch (Exception ex)
            {
            }
        }
    }
}