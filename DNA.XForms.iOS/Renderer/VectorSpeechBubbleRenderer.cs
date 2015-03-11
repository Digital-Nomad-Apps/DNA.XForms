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
					speechBubbleView = new Views.VectorSpeechBubbleUIView();
					SetViewProperties(speechBubble);
					speechBubbleView.Layer.SetShadowPropertiesOnLayer(speechBubble.HasShadow);

					SetNativeControl (speechBubbleView);


					// Speech bubble should be behind any of the Content views
					this.SendSubviewToBack(speechBubbleView);
				}
				catch (Exception ex) {
					// Usually: Image not found
					Console.WriteLine ("SpeechBubbleViewRenderer.OnElementChanged error: {0}", ex.Message);

					if (System.Diagnostics.Debugger.IsAttached || ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.SIMULATOR) {
						// Render something for debugging
						SetNativeControl (new UIView { BackgroundColor = Color.Red.ToUIColor () });
					} else {
						// Rethrow the exception
						throw;
					}
				}
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var speechBubble = this.Element;
			if (speechBubble != null) {

				Console.WriteLine ("SpeechBubbleViewRenderer.OnElementPropertyChanged({0})", e.PropertyName);

				// If any properties that affect the display are changed, we need to re-render
				if (e.PropertyName == VectorSpeechBubble.ArrowDirectionProperty.PropertyName ||
				   e.PropertyName == VectorSpeechBubble.BorderColorProperty.PropertyName ||
				   e.PropertyName == VectorSpeechBubble.BorderWidthProperty.PropertyName ||
				   e.PropertyName == VectorSpeechBubble.FillColorProperty.PropertyName ||
				   e.PropertyName == VectorSpeechBubble.GradientFillColorProperty.PropertyName ||
				   e.PropertyName == View.BackgroundColorProperty.PropertyName ||
				   e.PropertyName == View.WidthProperty.PropertyName ||
				   e.PropertyName == View.HeightProperty.PropertyName ||
				   e.PropertyName == VectorSpeechBubble.CornerRadiusProperty.PropertyName ||
				   e.PropertyName == VectorSpeechBubble.TextProperty.PropertyName) {


					Console.WriteLine ("SpeechBubbleViewRenderer.OnElementPropertyChanged({0})", e.PropertyName);

					// Update properties on the UIView and force it to redraw
					SetViewProperties (speechBubble);
					speechBubbleView.SetNeedsDisplay ();
				}
				if (e.PropertyName == VectorSpeechBubble.HasShadowProperty.PropertyName) {
					speechBubbleView.Layer.SetShadowPropertiesOnLayer(speechBubble.HasShadow);
				}
			}
		}

		private void SetViewProperties(VectorSpeechBubble speechBubble) {
			speechBubbleView.ArrowDirection = speechBubble.ArrowDirection;
			speechBubbleView.BorderColor = speechBubble.BorderColor == Color.Default ? Color.Transparent.ToUIColor () : speechBubble.BorderColor.ToUIColor ();
			speechBubbleView.BorderWidth = speechBubble.BorderColor == Color.Default ? 0f : (float)speechBubble.BorderWidth;
			speechBubbleView.CornerRadius = (float)speechBubble.CornerRadius;
			speechBubbleView.FillColor = speechBubble.FillColor == Color.Default ? UIColor.White : speechBubble.FillColor.ToUIColor ();
			// Default the Gradient Fill color to the same as the fill color (i.e. meaning no gradient)
			speechBubbleView.GradientFillColor = speechBubble.GradientFillColor == Color.Default ? speechBubbleView.FillColor : speechBubble.GradientFillColor.ToUIColor();
		
			if (speechBubble.BackgroundColor == Color.Default) {
				speechBubbleView.BackgroundColor = Color.Transparent.ToUIColor();
			}
			else {
				speechBubbleView.BackgroundColor = speechBubble.BackgroundColor.ToUIColor();
			}
		}
	}
}

