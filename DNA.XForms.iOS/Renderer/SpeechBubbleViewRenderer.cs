using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DNA.XForms;
using UIKit;

[assembly: ExportRenderer(typeof(SpeechBubbleView), typeof(DNA.XForms.iOS.Renderer.SpeechBubbleViewRenderer))]

namespace DNA.XForms.iOS.Renderer
{
	public class SpeechBubbleViewRenderer : ViewRenderer<SpeechBubbleView, UIView>
	{
		protected override void OnElementChanged (ElementChangedEventArgs<SpeechBubbleView> e)
		{
			base.OnElementChanged (e);

			var speechBubble = e.NewElement;
			if (speechBubble != null) 
			{
				try
				{
					if (!string.IsNullOrEmpty(speechBubble.ImageResource)) {
						var image = CreateUIImage(speechBubble);
						var imageView = new UIImageView(image);

						SetNativeControl (imageView);

						// Speech bubble should be behind any of the Content views
						this.SendSubviewToBack(imageView);
					}
					else
					{
						Console.WriteLine("SpeechBubbleViewRenderer: no image resource specified");
						// No image specified: use a rounded rectangle instead (TODO: Its doing right-angled corners at the moment)
						SetNativeControl(new UIView { BackgroundColor = speechBubble.BubbleColor.ToUIColor() });
					}

				}
				catch (Exception ex) {
					// Usually: Image not found
					Console.WriteLine ("SpeechBubbleViewRenderer.OnElementChanged error: {0}", ex.Message);

					// Render something for debugging
					SetNativeControl (new UIView { BackgroundColor = Color.Red.ToUIColor() });
				}
			}
		}

		private UIImage CreateUIImage(SpeechBubbleView speechBubble) {
			bool flipHorizontally = false;
			bool flipVertically = false;
			Thickness capWidth = new Thickness (-1d); // TODO

			return ImageHelper.CreateCappedUIImage (speechBubble.ImageResource, 
													flipHorizontally, 
													flipVertically, 
													speechBubble.BubbleColor, 
													capWidth);
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			Console.WriteLine ("SpeechBubbleViewRenderer.OnElementPropertyChanged({0})", e.PropertyName);

			// If any properties that affect the display are changed, we need to re-render
			if (e.PropertyName == SpeechBubbleView.ImageResourceProperty.PropertyName ||
				e.PropertyName == SpeechBubbleView.BubbleColorProperty.PropertyName ||
				e.PropertyName == SpeechBubbleView.ArrowDirectionProperty.PropertyName) {

				var speechBubbleView = this.Element;
				if (speechBubbleView != null) {

					// TODO: Implement this somehow to prevent multiple layout passes when setting a bunch of properties
					// if (speechBubbleView.LayoutPaused)
					//	return;

					var image = CreateUIImage (speechBubbleView);
					var imageView = this.Control as UIImageView;
					if (imageView == null) {
						this.SetNativeControl(new UIImageView(image));
					} else {
						imageView.Image = image;
					}

					SetNeedsDisplay ();
				}
			}
		}
	}
}

