using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DNA.XForms;
using UIKit;

[assembly: ExportRenderer(typeof(VectorSpeechBubble), typeof(DNA.XForms.iOS.Renderer.VectorSpeechBubbleRenderer))]

namespace DNA.XForms.iOS.Renderer
{
	public class VectorSpeechBubbleRenderer : ViewRenderer<VectorSpeechBubble, UIView>
	{
		private Views.VectorSpeechBubbleUIView speechBubbleView;

		protected override void OnElementChanged (ElementChangedEventArgs<VectorSpeechBubble> e)
		{
			base.OnElementChanged (e);

			var speechBubble = e.NewElement;
			if (speechBubble != null) 
			{
				try
				{
					/*
					if (!string.IsNullOrEmpty(speechBubble.ImageResource)) {
						Console.WriteLine("SpeechBubbleViewRenderer: image resource specified.  Drawing a scalable speech-bubble image");

						var image = CreateUIImage(speechBubble);
						speechBubbleView = new UIImageView(image);
					}
					else
					{
					*/

					Console.WriteLine("SpeechBubbleViewRenderer: no image resource specified.  Drawing a vector speech-bubble");

					speechBubbleView = new Views.VectorSpeechBubbleUIView {
						BorderColor = UIColor.White,
						FillColor = speechBubble.FillColor.ToUIColor(),
						BorderWidth = 4f,
					};

					//}

					SetNativeControl (speechBubbleView);

					// Speech bubble should be behind any of the Content views
					this.SendSubviewToBack(speechBubbleView);
				}
				catch (Exception ex) {
					// Usually: Image not found
					Console.WriteLine ("SpeechBubbleViewRenderer.OnElementChanged error: {0}", ex.Message);

					// Render something for debugging
					SetNativeControl (new UIView { BackgroundColor = Color.Red.ToUIColor() });
				}
			}
		}

		/*
		 * //TODO: Move to seperate renderer for Image-based speech bubble 
		private UIImage CreateUIImage(VectorSpeechBubble speechBubble) {
			bool flipHorizontally = false;
			bool flipVertically = false;
			Thickness capWidth = new Thickness (-1d); // TODO

			return ImageHelper.CreateCappedUIImage (speechBubble.ImageResource, 
													flipHorizontally, 
													flipVertically, 
													speechBubble.BubbleColor, 
													capWidth);
		}
		*/

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			Console.WriteLine ("SpeechBubbleViewRenderer.OnElementPropertyChanged({0})", e.PropertyName);

			// If any properties that affect the display are changed, we need to re-render
			if (e.PropertyName == VectorSpeechBubble.ArrowDirectionProperty.PropertyName ||
				e.PropertyName == VectorSpeechBubble.BorderColorProperty.PropertyName ||
				e.PropertyName == VectorSpeechBubble.BorderWidthProperty.PropertyName ||
				e.PropertyName == VectorSpeechBubble.FillColorProperty.PropertyName ||
				e.PropertyName == VectorSpeechBubble.GradientFillColorProperty.PropertyName) {

				var speechBubble = this.Element;
				if (speechBubble != null) {
			
					// Update properties on the UIView and force it to redraw
					speechBubbleView.ArrowDirection = speechBubble.ArrowDirection;
					speechBubbleView.BorderColor = speechBubble.BorderColor.ToUIColor();
					speechBubbleView.BorderWidth = (float)speechBubble.BorderWidth;
					speechBubbleView.FillColor = speechBubble.FillColor.ToUIColor();
					speechBubbleView.GradientFillColor = speechBubble.GradientFillColor.ToUIColor();

					SetNeedsDisplay ();
				}
			}
		}
	}
}

