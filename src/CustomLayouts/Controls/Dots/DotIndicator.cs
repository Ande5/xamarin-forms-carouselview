﻿using System;
using System.Collections;
using System.Linq;
using CustomLayouts.Controls.Interface;
using Xamarin.Forms;

namespace CustomLayouts.Controls.Dots
{
    /// <summary>
    /// dot style indicator
    /// </summary>
	public class DotIndicator : StackLayout , BaseIndicator
    {
		int _selectedIndex;

        public int SelectedIndex => _selectedIndex;

        public Color DotColor { get; set; }
		public double DotSize { get; set; }

		public DotIndicator()
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand;
			VerticalOptions = LayoutOptions.Center;
			Orientation = StackOrientation.Horizontal;
			DotColor = Color.Black;
		}

		void CreateDot()
		{
			//Make one button and add it to the dotLayout
			var dot = new Button {
				BorderRadius = Convert.ToInt32(DotSize/2),
				HeightRequest = DotSize,
				WidthRequest = DotSize,
				BackgroundColor = DotColor
			};
			Children.Add(dot);
		}

		public void CreateTabs()
		{
			foreach(var item in ItemsSource)
			{
				var tab = item as ITabProvider;
				var image = new Image {
					HeightRequest = 42,
					WidthRequest = 42,
					BackgroundColor = DotColor,
					Source = tab.ImageSource,
				};
				Children.Add(image);
			}
		}

		public static BindableProperty ItemsSourceProperty =
			BindableProperty.Create(
				nameof(ItemsSource),
				typeof(IList),
				typeof(DotIndicator),
				null,
				BindingMode.OneWay,
				propertyChanging: (bindable, oldValue, newValue) =>
				{
					((DotIndicator)bindable).ItemsSourceChanging();
				},
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((DotIndicator)bindable).ItemsSourceChanged();
				}
		);

		public IList ItemsSource {
			get {
				return (IList)GetValue(ItemsSourceProperty);
			}
			set {
				SetValue (ItemsSourceProperty, value);
			}
		}

		public static BindableProperty SelectedItemProperty =
			BindableProperty.Create(
				nameof(SelectedItem),
				typeof(object),
				typeof(DotIndicator),
				null,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((DotIndicator)bindable).SelectedItemChanged();
				}
		);

		public object SelectedItem {
			get {
				return GetValue (SelectedItemProperty);
			}
			set {
				SetValue (SelectedItemProperty, value);
			}
		}

        public void ItemsSourceChanging ()
		{
			if (ItemsSource != null)
				_selectedIndex = ItemsSource.IndexOf (SelectedItem);
		}

        public void ItemsSourceChanged ()
		{
			if (ItemsSource == null) return;

			// Dots *************************************
			var countDelta = ItemsSource.Count - Children.Count;

			if (countDelta > 0) {
				for (var i = 0; i < countDelta; i++) 
				{
					CreateDot();
				}
			} 
			else if (countDelta < 0) 
			{
				for (var i = 0; i < -countDelta; i++) 
				{
					Children.RemoveAt (0);
				}
			}
			//*******************************************
		}

        public void SelectedItemChanged () {

			var selectedIndex = ItemsSource.IndexOf (SelectedItem);
			var pagerIndicators = Children.Cast<Button> ().ToList ();

			foreach(var pi in pagerIndicators)
			{
				UnselectDot(pi);
			}

			if(selectedIndex > -1)
			{
				SelectDot(pagerIndicators[selectedIndex]);
			}
		}

		static void UnselectDot (Button dot)
		{
			dot.Opacity = 0.5;
		}

		static void SelectDot (Button dot)
		{
			dot.Opacity = 1.0;
		}
	}
}
