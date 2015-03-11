using System;

using Xamarin.Forms;

namespace DNA.XForms.Sample
{
	public class TextEntryPanel : ContentView
	{
		public static BindableProperty EnterButtonTextProperty = BindableProperty.Create<TextEntryPanel,string>(p => p.EnterButtonText, "Enter");
		public static BindableProperty PlaceholderProperty = BindableProperty.Create<TextEntryPanel,string>(p => p.Placeholder, "Enter message");

		public string EnterButtonText {
			get { return (string)base.GetValue (EnterButtonTextProperty); }
			set { base.SetValue (EnterButtonTextProperty, value); }
		}

		public string Placeholder {
			get { return (string)base.GetValue (PlaceholderProperty); }
			set { base.SetValue (PlaceholderProperty, value); }
		}

		/// <summary>
		/// A control containing an Entry control and a customizable "Enter" Button
		/// </summary>
		public TextEntryPanel ()
		{
			var enterButton = new Button { 
				// Text = "Speak", // Set using Binding
				Font = Font.SystemFontOfSize(NamedSize.Small),
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				HeightRequest = 30,
				IsEnabled = false, // Until some text is entered
			};
			var textEntry = new Entry { 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Keyboard = Keyboard.Chat,
				// Placeholder = "Enter message", // Set using Binding
				// Font = Font.SystemFontOfSize(NamedSize.Small), // Not supported in default Entry control
				HeightRequest = 30,
			};
			textEntry.TextChanged += (object sender, TextChangedEventArgs e) => {
				enterButton.IsEnabled = !string.IsNullOrEmpty(textEntry.Text);
			};
			textEntry.Completed += (object sender, EventArgs e) => {
				
				// TODO: Raise event to indicate that text was "Entered"

				textEntry.Text = "";
			};;


			enterButton.Clicked += (object sender, EventArgs e) => {
				// TODO: Raise event to indicate that text was "Entered"

				textEntry.Text = "";
				textEntry.Unfocus();  // dismisses the keyboard
			};

			var textEntryPanel = new StackLayout { 
				Padding = new Thickness (4),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Color.Gray.MultiplyAlpha (0.6d),
				Children = {
					textEntry,
					enterButton,
				},
			};

			this.Content = textEntryPanel;

			// Add Bindings
			enterButton.SetBinding(Button.TextProperty, new Binding("EnterButtonText", BindingMode.TwoWay, source:this));
			textEntry.SetBinding(Entry.PlaceholderProperty, new Binding("Placeholder", BindingMode.TwoWay, source:this));
		}
	}
}


