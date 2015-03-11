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

		public class TextEnteredEventArgs { public string Text {get;set;}}
		public event EventHandler<TextEnteredEventArgs> TextEntered;

		public event EventHandler Focused;

		private Entry _textEntry;

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
			_textEntry = new Entry { 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Keyboard = Keyboard.Chat,
				// Placeholder = "Enter message", // Set using Binding
				// Font = Font.SystemFontOfSize(NamedSize.Small), // Not supported in default Entry control
				HeightRequest = 30,
			};

			/*
			_textEntry.Focused += (sender, e) => {
				if (this.Focused != null) // Bubble the event up to the parent view
			-		this.Focused (this, new FocusEventArgs (this, true));
			};
			*/
	
			_textEntry.TextChanged += (sender, e) => {
				enterButton.IsEnabled = !string.IsNullOrEmpty(_textEntry.Text);
			};
			_textEntry.Completed += (sender, e) => OnTextEntered();
			enterButton.Clicked += (sender, e) => OnTextEntered();

			var textEntryPanel = new StackLayout { 
				Padding = new Thickness (4),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = ColorHelper.MyLightGray.ToFormsColor().MultiplyAlpha (0.9d), // TODO: Make this configurable
				Children = {
					_textEntry,
					enterButton,
				},
			};

			this.Content = textEntryPanel;

			// Add Bindings
			enterButton.SetBinding(Button.TextProperty, new Binding("EnterButtonText", BindingMode.TwoWay, source:this));
			_textEntry.SetBinding(Entry.PlaceholderProperty, new Binding("Placeholder", BindingMode.TwoWay, source:this));
		}

		private void OnTextEntered() {
			var eventArgs = new TextEnteredEventArgs { Text = _textEntry.Text };
			_textEntry.Text =  "";

			// Raise event to indicate that text was "Entered"
			if (this.TextEntered != null) {
				this.TextEntered(this, eventArgs);
			}
			_textEntry.Unfocus ();
		}
	}
}


