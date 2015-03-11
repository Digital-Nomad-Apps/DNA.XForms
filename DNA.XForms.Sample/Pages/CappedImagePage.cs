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

			Dictionary<string, Thickness> images = new Dictionary<string, Thickness> {
				{ "speechbubble.png", new Thickness (21d, 17d, 26.5d, 17.5d) },
				{ "speechbubble_fancy_green.png", new Thickness (21f, 14f, 21f, 14f) }, 
				{ "speechbubble_fancy_gray.png", new Thickness (21f, 14f, 21f, 14f) }, 
				{ "button_normal.png", new Thickness (6d, 6d, 6d, 6d) }, 
				{ "button_back.png", new Thickness (13d, 2d, 6d, 2d) }, 
			};

			var cappedImage = new CappedImage (images.First().Key, images.First().Value);
			cappedImage.HorizontalOptions = LayoutOptions.Start;

			var imagePicker = new Picker {
				Title = "Image",
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};
			foreach (var item in images) {
				imagePicker.Items.Add (item.Key);
			}
			imagePicker.SelectedIndex = 0;
			imagePicker.SelectedIndexChanged += (sender, e) => {
				var selectedItem = imagePicker.Items[imagePicker.SelectedIndex];
			
				// Set these in a single call to prevent 2 layout calls (one of which will be a bit weird because the CapWidth won't match the ImageResource
				cappedImage.SetImageAndCapWidth(selectedItem, images[selectedItem]);
			};

			var flipVerticallySwitch = new Switch { HorizontalOptions = LayoutOptions.End};
			var flipHorizontallySwitch = new Switch { HorizontalOptions = LayoutOptions.End};
			var colorPicker = new ColorPicker { 
				HorizontalOptions = LayoutOptions.FillAndExpand
			};


			colorPicker.SelectedColorChanged += (sender, args) =>
			{
				cappedImage.TintColor = colorPicker.SelectedColor;
				colorPicker.BackgroundColor = colorPicker.SelectedColor; 
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
				Content = new StackLayout {
					Padding = new Thickness (8d, 8d, 8d, 8d),
					Spacing = 8d,
					Children = {
						imagePicker,
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
									Text = "Flip Vertically",
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
									Text = "Flip Horizontally",
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

