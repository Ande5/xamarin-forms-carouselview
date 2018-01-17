using System;
using Xamarin.Forms;
using CustomLayouts.Controls;
using CustomLayouts.ViewModels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

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
		RelativeLayout relativeLayout;

		IndicatorStyleEnum _indicatorStyle;

		SwitcherPageViewModel viewModel;

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
                        tabbedView = new TopIndicatorTabbedView();
                        break;
                    case IndicatorStyleEnum.None:
                        tabbedView = new TopIndicatorTabbedView();
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

