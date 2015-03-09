using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics;
using DNA.XForms.Sample.ViewModels;

namespace DNA.XForms.Sample
{

	public class MenuPage : ContentPage
	{
		readonly List<DNA.XForms.Sample.ViewModels.MenuItem> MenuItems = new List<DNA.XForms.Sample.ViewModels.MenuItem>();

		public ListView Menu { get; set; }

		public MenuPage ()
		{
			MenuItems.Add (new AboutMenuItem ());
			MenuItems.Add (new CappedImageMenuItem ());
			MenuItems.Add(new SpeechBubbleMenuItem());

			BackgroundColor = ColorHelper.MenuBackgroundColor;

			var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

			var headerText = new ContentView {
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Xamarin.Forms.Label {
					TextColor = Color.White,
					Text = "DNA.XForms Sample", 
					Font = Font.SystemFontOfSize(NamedSize.Medium),
				}
			};

			layout.Children.Add(headerText);

			Menu = new ListView {
				ItemsSource = MenuItems,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent,
				RowHeight = 48,
			};

			var cell = new DataTemplate(typeof(ImageCell));
			cell.SetBinding(TextCell.TextProperty, "Title");
			cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");
			cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

			Menu.ItemTemplate = cell;

			layout.Children.Add(Menu);

			this.Content = layout;
		}
	}

}

