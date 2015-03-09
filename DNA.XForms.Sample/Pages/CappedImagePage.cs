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
			this.Title = "Capped Image Sample";

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

			var cappedImage = new CappedImage (@"MessageBubble.png", caps);
			cappedImage.HorizontalOptions = LayoutOptions.Start;

			var flipVerticallySwitch = new Switch { HorizontalOptions = LayoutOptions.End};
			var flipHorizontallySwitch = new Switch { HorizontalOptions = LayoutOptions.End};
			var colorPicker = new Picker { 
				Title = "Color",
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			foreach (var item in NamedColors.Select (n => n.Key).ToList ()) {
				colorPicker.Items.Add (item);
			}
			colorPicker.SelectedIndex = 0; // None (Default)

			colorPicker.SelectedIndexChanged += (sender, args) =>
			{
				if (colorPicker.SelectedIndex == -1)
				{
					cappedImage.TintColor = Color.Default;
					colorPicker.BackgroundColor = Color.Default;
				}
				else
				{
					string colorName = colorPicker.Items[colorPicker.SelectedIndex];
					var color = NamedColors[colorName];
					cappedImage.TintColor = color;
					colorPicker.BackgroundColor = color; 
				}
			};

			var widthSlider = new Slider {
				Minimum = 0d,
				Maximum = Application.Current.MainPage.Width-20f,
				Value = 100d,
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};
			var heightSlider = new Slider {
				Minimum = 0d,
				Maximum = Application.Current.MainPage.Height,
				Value = 50d,
				HorizontalOptions = LayoutOptions.EndAndExpand,
			};

			this.Content = new ScrollView {
				Content = new StackLayout () {
					Padding = new Thickness (8d, 8d, 8d, 8d),
					Spacing = 8d,
					Children = {
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Spacing = 16,
							Children = {
								new Label { Text = "Color", HorizontalOptions = LayoutOptions.Start, YAlign = TextAlignment.Center },
								colorPicker,
							},
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label {
									Text = "Switch Vertically",
									HorizontalOptions = LayoutOptions.StartAndExpand,
									YAlign = TextAlignment.Center
								},
								flipVerticallySwitch,
							},
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label {
									Text = "Switch Horizontally",
									HorizontalOptions = LayoutOptions.StartAndExpand,
									YAlign = TextAlignment.Center
								},
								flipHorizontallySwitch,
							},
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Width", HorizontalOptions = LayoutOptions.StartAndExpand, YAlign = TextAlignment.Center },
								widthSlider,
							},
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Height", HorizontalOptions = LayoutOptions.StartAndExpand, YAlign = TextAlignment.Center },
								heightSlider,
							},
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								cappedImage,
								new BoxView {
									HorizontalOptions = LayoutOptions.EndAndExpand,
								},
							}
						}

					},
				}
			};

			cappedImage.SetBinding(CappedImage.FlippedHorizontallyProperty, new Binding("IsToggled", BindingMode.OneWay, source:flipHorizontallySwitch));
			cappedImage.SetBinding(CappedImage.FlippedVerticallyProperty, new Binding("IsToggled", BindingMode.OneWay, source:flipVerticallySwitch));
			cappedImage.SetBinding(CappedImage.WidthRequestProperty, new Binding("Value", BindingMode.TwoWay, source:widthSlider));
			cappedImage.SetBinding(CappedImage.HeightRequestProperty, new Binding("Value", BindingMode.TwoWay, source:heightSlider));
		}
	}
}

