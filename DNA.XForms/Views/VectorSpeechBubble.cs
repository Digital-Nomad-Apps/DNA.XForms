using System;
using Xamarin.Forms;

namespace DNA.XForms
{
	/// <summary>
	/// Speech bubble view containing a vector speech bubble, overlaid with some Content which can be text, or other controls or layouts
	/// </summary>
	public class VectorSpeechBubble : ContentView
	{
		#region Nested Types

		public enum ArrowDirections {
			None = 0,

			UpLeft = 1,
			UpCenter = 2,
			UpRight = 3,

			DownLeft = 4,
			DownCenter = 5,
			DownRight = 6,

			LeftTop = 7,
			LeftCenter = 8,
			LeftBottom = 9,

			RightTop = 10,
			RightCenter = 11,
			RightBottom = 12,
		}

		#endregion

		#region BindableProperties

		public static readonly BindableProperty ArrowDirectionProperty = BindableProperty.Create<VectorSpeechBubble, ArrowDirections>(p => p.ArrowDirection, ArrowDirections.DownRight);
		public static readonly BindableProperty ArrowOffsetProperty = BindableProperty.Create<VectorSpeechBubble, double>(p => p.ArrowOffset, 0f);

		public static readonly BindableProperty FillColorProperty = BindableProperty.Create<VectorSpeechBubble, Color>(p => p.FillColor, Color.Default);
		public static readonly BindableProperty GradientFillColorProperty = BindableProperty.Create<VectorSpeechBubble, Color>(p => p.GradientFillColor, Color.Default);
		public static readonly BindableProperty BorderColorProperty = BindableProperty.Create<VectorSpeechBubble, Color>(p => p.BorderColor, Color.Default);

		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create<VectorSpeechBubble, double>(p => p.BorderWidth, 2d);

		public static readonly BindableProperty TextProperty = BindableProperty.Create<VectorSpeechBubble, string>(p => p.Text, "");
		public static readonly BindableProperty IsTypingProperty = BindableProperty.Create<VectorSpeechBubble, bool>(p => p.IsTyping, false);

		public ArrowDirections ArrowDirection {
			get { return (ArrowDirections)base.GetValue (ArrowDirectionProperty); } 
			set { base.SetValue (ArrowDirectionProperty, value); }
		}

		/// <summary>
		/// Offset from the nearest corner of the arrow
		/// </summary>
		public double ArrowOffset {
			get { return (double)base.GetValue (ArrowOffsetProperty); } 
			set { base.SetValue (ArrowOffsetProperty, value); }
		}

		public Color FillColor {
			get { return (Color)base.GetValue (FillColorProperty); } 
			set { base.SetValue (FillColorProperty, value); }
		}

		public double BorderWidth {
			get { return (double)base.GetValue (BorderWidthProperty); } 
			set { base.SetValue (BorderWidthProperty, value); }
		}

		/// <summary>
		/// Allows specifying a secondary color for a gradient effect
		/// // TODO: How to specify the path of the gradient
		/// </summary>
		public Color GradientFillColor {
			get { return (Color)base.GetValue (GradientFillColorProperty); } 
			set { base.SetValue (GradientFillColorProperty, value); }
		}

		public Color BorderColor {
			get { return (Color)base.GetValue (BorderColorProperty); } 
			set { base.SetValue (BorderColorProperty, value); }
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

		/// <summary>
		/// Creates a new SpeechBubble view
		/// </summary>
		/// <param name="arrowDirection">Arrow direction.</param>
		public VectorSpeechBubble (ArrowDirections arrowDirection = ArrowDirections.DownRight)
		{
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

	public static class ArrowDirectionsExtensions {
		public static bool IsUp(this VectorSpeechBubble.ArrowDirections direction) {
			return (direction == VectorSpeechBubble.ArrowDirections.UpLeft ||
				direction == VectorSpeechBubble.ArrowDirections.UpCenter ||
				direction == VectorSpeechBubble.ArrowDirections.UpRight);
		}
		public static bool IsDown(this VectorSpeechBubble.ArrowDirections direction) {
			return (direction == VectorSpeechBubble.ArrowDirections.DownLeft ||
				direction == VectorSpeechBubble.ArrowDirections.DownCenter ||
				direction == VectorSpeechBubble.ArrowDirections.DownRight);
		}
		public static bool IsLeft(this VectorSpeechBubble.ArrowDirections direction) {
			return (direction == VectorSpeechBubble.ArrowDirections.LeftTop ||
				direction == VectorSpeechBubble.ArrowDirections.LeftCenter ||
				direction == VectorSpeechBubble.ArrowDirections.LeftBottom);
		}
		public static bool IsRight(this VectorSpeechBubble.ArrowDirections direction) {
			return (direction == VectorSpeechBubble.ArrowDirections.RightTop ||
				direction == VectorSpeechBubble.ArrowDirections.RightCenter ||
				direction == VectorSpeechBubble.ArrowDirections.RightBottom);
		}
	}
}

