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
            viewModel = new SwitcherPageViewModel();
            BindingContext = viewModel;
            Title = _indicatorStyle.ToString();

            switch (indicatorStyle)
            {
                case IndicatorStyleEnum.Dots:
                    Content = new DotTabbedView();
                    break;
                case IndicatorStyleEnum.Tabs:
                    Content = new TopIndicatorTabbedView();
                    break;
                case IndicatorStyleEnum.None:
                    Content = new TopIndicatorTabbedView();
                    break;
            }
        }
	}
}

