using Xamarin.Forms;
using DNA.XForms;

namespace DNA.XForms.Sample
{
	public class VectorSpeechBubblePage : ContentPage
	{
		public VectorSpeechBubblePage ()
		{
			this.Title = "Vector Speech Bubble";
			this.BackgroundColor = ColorHelper.MyLightBlue.ToFormsColor();

			var bubble = new VectorSpeechBubble {
				Text = "Hello there. Do you have a minute to talk?",
				ArrowDirection = VectorSpeechBubble.ArrowDirections.RightBottom,
				BorderColor = Color.White,
				BorderWidth = 4d,
				Padding = new Thickness(8d,8d,8d,8d),	// TODO: Auto calculate padding based on the arrow direction and size.  This padding should be in addition to
				HasShadow = true,
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Start,
			};

			var arrowDirectionPicker = new Picker { 
				HorizontalOptions = LayoutOptions.End, 
				WidthRequest=150d,
			};
			foreach (var direction in System.Enum.GetNames(typeof(VectorSpeechBubble.ArrowDirections))) {
				arrowDirectionPicker.Items.Add (direction);
			}
			arrowDirectionPicker.SelectedIndex = arrowDirectionPicker.Items.IndexOf (VectorSpeechBubble.ArrowDirections.RightBottom.ToString());

			arrowDirectionPicker.SelectedIndexChanged += (object sender, System.EventArgs e) => {
				var selectedValue = arrowDirectionPicker.Items[arrowDirectionPicker.SelectedIndex];
				var direction = (VectorSpeechBubble.ArrowDirections)System.Enum.Parse(typeof(VectorSpeechBubble.ArrowDirections), selectedValue);
				bubble.ArrowDirection = direction;
			};

			var shadowSwitch = new Switch() { HorizontalOptions = LayoutOptions.End, WidthRequest=150d };

			var fillColorPicker = new ColorPicker () { HorizontalOptions = LayoutOptions.End, WidthRequest=150d };
			fillColorPicker.SelectedColor = Color.Silver;

			var gradientColorPicker = new ColorPicker () { HorizontalOptions = LayoutOptions.End, WidthRequest=150d };
			gradientColorPicker.SelectedColor = Color.Default;

			var borderColorPicker = new ColorPicker () { HorizontalOptions = LayoutOptions.End, WidthRequest=150d };
			borderColorPicker.SelectedColor = Color.Purple;

			var borderWidthSlider = new Slider (0d, 100d, 4d) { HorizontalOptions = LayoutOptions.End, WidthRequest=150d };
			var cornerRadiusSlider = new Slider (0d, 50d, 10d) { HorizontalOptions = LayoutOptions.End, WidthRequest=150d };

			var widthSlider = new Slider {
				Minimum = 0d,
				Maximum = Application.Current.MainPage.Width - 20d,
				Value = Application.Current.MainPage.Width,
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 150d,
			};
			var heightSlider = new Slider {
				Minimum = 0d,
				Maximum = 300d,
				Value = 50d,
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 150d,
			};

			this.Content = new ScrollView {
				Content = new StackLayout {
					Padding = new Thickness(4d,4d,4d,4d),
					Spacing = 4d,
					Children = {
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Arrow Direction", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								arrowDirectionPicker,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Has Shadow", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								shadowSwitch,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Fill Color", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								fillColorPicker,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Gradient Color", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								gradientColorPicker,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Border Color", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								borderColorPicker,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Border Width", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								borderWidthSlider,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Corner Radius", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								cornerRadiusSlider,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Width", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								widthSlider,
							}
						},
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children = {
								new Label { Text = "Height", HorizontalOptions = LayoutOptions.FillAndExpand, YAlign = TextAlignment.Center },
								heightSlider,
							}
						},
						new BoxView { HeightRequest = 20d }, // For some additional spacing
						bubble,
					},
				}
			};

			bubble.SetBinding(VectorSpeechBubble.FillColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:fillColorPicker));
			bubble.SetBinding(VectorSpeechBubble.GradientFillColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:gradientColorPicker));
			bubble.SetBinding(VectorSpeechBubble.BorderColorProperty, new Binding("SelectedColor", BindingMode.OneWay, source:borderColorPicker));
			bubble.SetBinding(VectorSpeechBubble.BorderWidthProperty, new Binding("Value", BindingMode.TwoWay, source:borderWidthSlider));
			bubble.SetBinding(VectorSpeechBubble.CornerRadiusProperty, new Binding("Value", BindingMode.TwoWay, source:cornerRadiusSlider));
		
			bubble.SetBinding(VectorSpeechBubble.WidthRequestProperty, new Binding("Value", BindingMode.TwoWay, source:widthSlider));
			bubble.SetBinding(VectorSpeechBubble.HeightRequestProperty, new Binding("Value", BindingMode.TwoWay, source:heightSlider));
			bubble.SetBinding(VectorSpeechBubble.HasShadowProperty, new Binding("IsToggled", BindingMode.TwoWay, source:shadowSwitch));
		}
	}
}

