using Xamarin.Forms;
using System.Collections.Generic;

namespace DNA.XForms.Sample
{

	public class MenuPage : ContentPage
	{
		readonly List<ViewModels.MenuItem> MenuItems = new List<ViewModels.MenuItem>();

		public ListView Menu { get; set; }

		public MenuPage ()
		{
			MenuItems.AddRange (ViewModels.MenuItem.All());

			BackgroundColor = ColorHelper.MenuBackgroundColor;

			var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

			var headerText = new ContentView {
				Padding = new Thickness(10, 36, 0, 5),
				Content = new Xamarin.Forms.Label {
					TextColor = Color.White,
					Text = "DNA.XForms Sample", 
					Font = Font.SystemFontOfSize (NamedSize.Medium),
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

