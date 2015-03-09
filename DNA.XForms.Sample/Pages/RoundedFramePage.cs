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

			var shadowSwitch = new Switch { IsToggled = true };

			var originalFrame = new Frame { 
				Content = new Label { Text = "This is a regular frame" },
				BackgroundColor = Color.Red,
			};

			var roundedFrame = new RoundedFrame { 
				BackgroundColor = Color.Red,
				HasShadow = true,
				Content = new Label { Text = "This is a rounded frame" } 
			};
			var cornerRadiusSlider = new Slider (0d, 100d, 5d);

			var colorPicker = new ColorPicker ();

			Content = new StackLayout { 
				Padding = new Thickness(8d),
				Spacing = 4d,
				Children = {
					originalFrame,
					roundedFrame,
					new BoxView { HeightRequest=16d, BackgroundColor=Color.Transparent },
					new Label { Text = "Corner Radius" },
					cornerRadiusSlider,
					new Label { Text = "Shadows?" },
					shadowSwitch,
					new Label { Text = "Background Color" },
					colorPicker,
				}
			};

			roundedFrame.SetBinding(RoundedFrame.CornerRadiusProperty, new Binding("Value", BindingMode.TwoWay, source:cornerRadiusSlider));

			originalFrame.SetBinding(RoundedFrame.HasShadowProperty, new Binding("IsToggled", BindingMode.TwoWay, source:shadowSwitch));
			roundedFrame.SetBinding(RoundedFrame.HasShadowProperty, new Binding("IsToggled", BindingMode.TwoWay, source:shadowSwitch));

			originalFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:colorPicker));
			roundedFrame.SetBinding(Frame.BackgroundColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:colorPicker));
		}
	}
}


