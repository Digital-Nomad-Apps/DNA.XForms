using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;


namespace DNA.XForms.Sample
{
	public class CappedImagePage : ContentPage
	{
		public CappedImagePage ()
		{
			// Dictionary to get Color from color name.
			Dictionary<string, Color> NamedColors = new Dictionary<string, Color>
			{
				{ "None (Default)", Color.Default },
				{ "Aqua", Color.Aqua }, 
				{ "Black", Color.Black },
				{ "Blue", Color.Blue }, 
				{ "Gray", Color.Gray }, 
				{ "Green", Color.Green },
				{ "Lime", Color.Lime }, 
				{ "Maroon", Color.Maroon },
				{ "Navy", Color.Navy }, 
				{ "Olive", Color.Olive },
				{ "Purple", Color.Purple }, 
				{ "Red", Color.Red },
				{ "Silver", Color.Silver }, 
				{ "Teal", Color.Teal },
				{ "White", Color.White }, 
				{ "Yellow", Color.Yellow }
			};

			var caps = new Thickness (21d, 17d, 26.5d, 17.5d); 

			var image1 = new CappedImage (@"MessageBubble.png", caps);
			var image2 = new CappedImage (@"MessageBubble.png", caps);

			var flipVerticallySwitch = new Switch { HorizontalOptions = LayoutOptions.End};
			var flipHorizontallySwitch = new Switch { HorizontalOptions = LayoutOptions.End};
			var colorPicker = new Picker { 
				Title = "Color",
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			foreach (var item in NamedColors.Select (n => n.Key).ToList ()) {
				colorPicker.Items.Add (item);
			}
			colorPicker.SelectedIndexChanged += (sender, args) =>
			{
				if (colorPicker.SelectedIndex == -1)
				{
					image2.TintColor = Color.Default;
					colorPicker.BackgroundColor = Color.Default;
				}
				else
				{
					string colorName = colorPicker.Items[colorPicker.SelectedIndex];
					var color = NamedColors[colorName];
					image2.TintColor = color;
					colorPicker.BackgroundColor = color; 
				}
			};


			this.Content = new StackLayout() {
				Padding = new Thickness(8d,8d,8d,8d),
				Spacing = 8d,
				Children = {
					image1,
					image2,
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Spacing = 16,
						Children = {
							new Label { Text = "Color", HorizontalOptions = LayoutOptions.Start, YAlign=TextAlignment.Center},
							colorPicker,
						},
					},
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label { Text = "Switch Vertically", HorizontalOptions = LayoutOptions.StartAndExpand, YAlign=TextAlignment.Center},
							flipVerticallySwitch,
						},
					},
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label { Text = "Switch Horizontally", HorizontalOptions = LayoutOptions.StartAndExpand, YAlign=TextAlignment.Center},
							flipHorizontallySwitch,
						},
					}
				},
			};

			image2.SetBinding(CappedImage.FlippedHorizontallyProperty, new Binding("IsToggled", BindingMode.OneWay, source:flipHorizontallySwitch));
			image2.SetBinding(CappedImage.FlippedVerticallyProperty, new Binding("IsToggled", BindingMode.OneWay, source:flipVerticallySwitch));
		}
	}
}

