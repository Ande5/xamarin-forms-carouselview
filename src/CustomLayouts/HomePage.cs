using System;
using Xamarin.Forms;
using CustomLayouts.Controls;
using CustomLayouts.ViewModels;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace CustomLayouts
{
	public class HomePage : ContentPage
	{
		RelativeLayout relativeLayout;

		CarouselLayout.IndicatorStyleEnum _indicatorStyle;

		SwitcherPageViewModel viewModel;

		public HomePage(CarouselLayout.IndicatorStyleEnum indicatorStyle)
		{
            viewModel = new SwitcherPageViewModel();
            BindingContext = viewModel;
            Title = _indicatorStyle.ToString();

            switch (indicatorStyle)
            {
                case CarouselLayout.IndicatorStyleEnum.Dots:
                    Content = new DotTabbedView();
                    break;
                case CarouselLayout.IndicatorStyleEnum.Tabs:
                    Content = new TopIndicatorTabbedView();
                    break;
                case CarouselLayout.IndicatorStyleEnum.None:
                    Content = new TopIndicatorTabbedView();
                    break;
            }
            /*
			_indicatorStyle = indicatorStyle;

			

			relativeLayout = new RelativeLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var pagesCarousel = CreatePagesCarousel();

            //dots
			var dots = CreatePagerIndicatorContainer();
            //tabs
			var tabs = CreateTabs();

			switch(pagesCarousel.IndicatorStyle)
			{
				case CarouselLayout.IndicatorStyleEnum.Dots:
					relativeLayout.Children.Add (pagesCarousel,
						Constraint.RelativeToParent ((parent) => { return parent.X; }),
						Constraint.RelativeToParent ((parent) => { return parent.Y; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width; }),
						Constraint.RelativeToParent ((parent) => { return parent.Height/2; })
					);

					relativeLayout.Children.Add (dots, 
						Constraint.Constant (0),
						Constraint.RelativeToView (pagesCarousel, 
							(parent,sibling) => { return sibling.Height - 18; }),
						Constraint.RelativeToParent (parent => parent.Width),
						Constraint.Constant (18)
					);
					break;
				case CarouselLayout.IndicatorStyleEnum.Tabs:
					var tabsHeight = 25;
					relativeLayout.Children.Add (tabs, 
						Constraint.Constant (0),
						Constraint.Constant (0),
						Constraint.RelativeToParent (parent => parent.Width),
						Constraint.Constant (tabsHeight)
					);

					relativeLayout.Children.Add (pagesCarousel,
						Constraint.RelativeToParent ((parent) => { return parent.X; }),
						Constraint.RelativeToParent ((parent) => { return parent.Y + tabsHeight; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width; }),
						Constraint.RelativeToParent ((parent) => { return parent.Height - tabsHeight; })
					);
					break;
				default:
					relativeLayout.Children.Add (pagesCarousel,
						Constraint.RelativeToParent ((parent) => { return parent.X; }),
						Constraint.RelativeToParent ((parent) => { return parent.Y; }),
						Constraint.RelativeToParent ((parent) => { return parent.Width; }),
						Constraint.RelativeToParent ((parent) => { return parent.Height; })
					);
					break;
			}

			Content = relativeLayout;
            */
        }

        //PagesCarousel
        CarouselLayout CreatePagesCarousel ()
		{
			var carousel = new CarouselLayout {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				IndicatorStyle = _indicatorStyle,
				ItemTemplate = new DataTemplate(typeof(HomeView))
			};
			carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "Pages");
			carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentPage", BindingMode.TwoWay);

			return carousel;
		}

        //Dot
		View CreatePagerIndicatorContainer()
		{
			return new StackLayout {
				Children = { CreatePagerIndicators() }
			};
		}

		View CreatePagerIndicators()
		{
			var pagerIndicator = new PagerIndicatorDots() { DotSize = 5, DotColor = Color.Black };
			pagerIndicator.SetBinding (PagerIndicatorDots.ItemsSourceProperty, "Pages");
			pagerIndicator.SetBinding (PagerIndicatorDots.SelectedItemProperty, "CurrentPage");
			return pagerIndicator;
		}

        //Tab
		View CreateTabsContainer()
		{
			return new StackLayout {
				Children = { CreateTabs() }
			};
		}

		View CreateTabs()
		{
            var tabscrollView = new ScrollView();

			var pagerIndicator = new PagerIndicatorTabs();
            pagerIndicator.WidthRequest = 1000;

            pagerIndicator.RowDefinitions.Add(new RowDefinition() { Height = 25 });
			pagerIndicator.SetBinding (PagerIndicatorTabs.ItemsSourceProperty, "Pages");
			pagerIndicator.SetBinding (PagerIndicatorTabs.SelectedItemProperty, "CurrentPage");

            tabscrollView.Content = pagerIndicator;

            tabscrollView.ScrollToAsync(100, 0, true);

            tabscrollView.Orientation = ScrollOrientation.Horizontal;
            tabscrollView.WidthRequest = 250;

            return tabscrollView;
		}
	}

	public class SpacingConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var items = value as IEnumerable<HomeViewModel>;

			var collection = new ColumnDefinitionCollection();
			foreach(var item in items)
			{
				collection.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
			}
			return collection;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

