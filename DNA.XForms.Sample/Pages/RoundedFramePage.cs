using System;
using Xamarin.Forms;
using DNA.XForms;

namespace DNA.XForms.Sample
{
	public class RoundedFramePage : ContentPage
	{
		public RoundedFramePage ()
		{
			Title = "Rounded Frame Sample";
			this.BackgroundColor = ColorHelper.MyLightGray.ToFormsColor();

			var shadowSwitch = new Switch { IsToggled = true, HorizontalOptions = LayoutOptions.EndAndExpand };

			var originalFrame = new Frame { 
				Content = new Label { Text = "This is a regular frame" },
			};

			var roundedFrame = new RoundedFrame { 
				HasShadow = true,
				Content = new Label { Text = "This is a rounded frame" } 
			};
			var cornerRadiusSlider = new Slider (0d, 50d, 10d) { HorizontalOptions = LayoutOptions.EndAndExpand };
			var outlineWidthSlider = new Slider (0d, 50d, 1d) { HorizontalOptions = LayoutOptions.EndAndExpand };

			var borderColorPicker = new ColorPicker { SelectedColor = Color.Default };
			var outlineColorPicker = new ColorPicker { SelectedColor = Color.Default };

			Content = new ScrollView {
				Content = new StackLayout { 
					Padding = new Thickness(8d),
					Spacing = 4d,
					Children = {
						originalFrame,
						roundedFrame,
						new BoxView { HeightRequest=16d, BackgroundColor=Color.Transparent },
						new Label { Text = "Background Color" },
						borderColorPicker,
						new Label { Text = "Outline Color" },
						outlineColorPicker,
						new StackLayout { 
							Orientation = StackOrientation.Horizontal,
							Children = { 
								new Label { Text = "Corner Radius", HorizontalOptions = LayoutOptions.Start },
								cornerRadiusSlider,
							},
						},
						new StackLayout { 
							Orientation = StackOrientation.Horizontal,
							Children = { 
								new Label { Text = "Outline Width", HorizontalOptions = LayoutOptions.Start },
								outlineWidthSlider,
							},
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Shadows?", HorizontalOptions = LayoutOptions.Start },
								shadowSwitch,
							},
						},
					}
				}
			};

			roundedFrame.SetBinding(RoundedFrame.CornerRadiusProperty, new Binding("Value", BindingMode.TwoWay, source:cornerRadiusSlider));

			originalFrame.SetBinding(RoundedFrame.HasShadowProperty, new Binding("IsToggled", BindingMode.TwoWay, source:shadowSwitch));
			roundedFrame.SetBinding(RoundedFrame.HasShadowProperty, new Binding("IsToggled", BindingMode.TwoWay, source:shadowSwitch));

			originalFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:borderColorPicker));
			roundedFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:borderColorPicker));
		
			originalFrame.SetBinding(Frame.OutlineColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:outlineColorPicker));
			roundedFrame.SetBinding(Frame.OutlineColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:outlineColorPicker));

			roundedFrame.SetBinding(RoundedFrame.OutlineWidthProperty, new Binding("Value", BindingMode.TwoWay, source:outlineWidthSlider));
		}
	}
}


