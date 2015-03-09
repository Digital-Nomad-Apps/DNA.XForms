using System;
using Xamarin.Forms;

namespace DNA.XForms
{
	/// <summary>
	/// Speech bubble view containing a speech bubble image, overlaid with some Content which can be text, or other controls or layouts
	/// </summary>
	public class SpeechBubbleView : ContentView
	{
		#region Nested Types

		public enum ArrowDirections {
			LeftTop = 1,
			LeftBottom = 2,
			RightTop = 3,
			RightBottom = 4,
		}

		#endregion

		#region BindableProperties

		public static readonly BindableProperty ImageResourceProperty = BindableProperty.Create<SpeechBubbleView, string>(p => p.ImageResource, "");
		public static readonly BindableProperty ArrowDirectionProperty = BindableProperty.Create<SpeechBubbleView, ArrowDirections>(p => p.ArrowDirection, ArrowDirections.LeftBottom);
		public static readonly BindableProperty BubbleColorProperty = BindableProperty.Create<SpeechBubbleView, Color>(p => p.BubbleColor, Color.Gray);
		public static readonly BindableProperty TextProperty = BindableProperty.Create<SpeechBubbleView, string>(p => p.Text, "");
		public static readonly BindableProperty IsTypingProperty = BindableProperty.Create<SpeechBubbleView, bool>(p => p.IsTyping, false);

		public string ImageResource {
			get { return (string)base.GetValue (ImageResourceProperty); } 
			set { base.SetValue (ImageResourceProperty, value); }
		}

		public ArrowDirections ArrowDirection {
			get { return (ArrowDirections)base.GetValue (ArrowDirectionProperty); } 
			set { base.SetValue (ArrowDirectionProperty, value); }
		}

		public Color BubbleColor {
			get { return (Color)base.GetValue (BubbleColorProperty); } 
			set { base.SetValue (BubbleColorProperty, value); }
		}

		public string Text {
			get { return (string)base.GetValue (TextProperty); } 
			set { base.SetValue (TextProperty, value); }
		}

		public bool IsTyping {
			get { return (bool)base.GetValue (IsTypingProperty); } 
			set { base.SetValue (IsTypingProperty, value); }
		}

		#endregion

		public SpeechBubbleView (string imageResource = "", ArrowDirections arrowDirection = ArrowDirections.RightBottom)
		{
			this.ImageResource = imageResource;
			this.ArrowDirection = arrowDirection;
			

			this.SizeChanged += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine ("SpeechBubbleView.SizeChanged: {0}", this.Bounds.Size);	
			};

			this.LayoutChanged += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine ("SpeechBubbleView.LayoutChanged");	
			};
		}

		protected override void OnPropertyChanged (string propertyName)
		{
			base.OnPropertyChanged (propertyName);

			if (propertyName == TextProperty.PropertyName) {
				if (this.Content == null) {
					if (!string.IsNullOrEmpty (this.Text)) {
						this.Content = new Label();
					}
				}
				if (this.Content is Label) {
					var label = (Label)this.Content;
					label.Text = this.Text;
				}
			}
		}
	}
}

