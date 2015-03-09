using System;
using Xamarin.Forms;

namespace DNA.XForms
{
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

		public static readonly BindableProperty ArrowDirectionProperty = BindableProperty.Create<SpeechBubbleView, ArrowDirections>(p => p.ArrowDirection, ArrowDirections.LeftBottom);
		public static readonly BindableProperty BubbleColorProperty = BindableProperty.Create<SpeechBubbleView, Color>(p => p.BubbleColor, Color.Gray);

		public ArrowDirections ArrowDirection {
			get { return (ArrowDirections)base.GetValue (ArrowDirectionProperty); } 
			set { base.SetValue (ArrowDirectionProperty, value); }
		}

		public Color BubbleColor {
			get { return (Color)base.GetValue (BubbleColorProperty); } 
			set { base.SetValue (BubbleColorProperty, value); }
		}

		#endregion

		public SpeechBubbleView (string bubbleImageFile, ArrowDirections initialOrientation = ArrowDirections.RightBottom)
		{
			// Load the image

			// Correctly orientate the image by flipping horizontally and/or vertically if required

			// Load the xml metadata file 

			// Set the padding

			// Events for layout diagnostics

			this.SizeChanged += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine ("SpeechBubbleView.SizeChanged: {0}", this.Bounds.Size);	
			};

			this.LayoutChanged += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine ("SpeechBubbleView.LayoutChanged");	
			};
		}
	}
}

