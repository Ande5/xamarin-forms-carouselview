using System;
using CustomLayouts.Controls;
using CustomLayouts.ViewModels;
using Xamarin.Forms;

namespace CustomLayouts
{
    public enum IndicatorStyleEnum
    {
        None,
        Dots,
        Tabs
    }

    public class HomePage : ContentPage
    {
        private IndicatorStyleEnum _indicatorStyle;
        private RelativeLayout relativeLayout;

        private readonly SwitcherPageViewModel viewModel;

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