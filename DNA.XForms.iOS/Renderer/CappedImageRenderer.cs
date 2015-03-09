using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

using DNA.XForms;

[assembly: ExportRenderer(typeof(CappedImage), typeof(DNA.XForms.iOS.Renderer.CappedImageRenderer))]

namespace DNA.XForms.iOS.Renderer
{
	public class CappedImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged (e);

			var cappedImage = e.NewElement as CappedImage;
			if (cappedImage != null) 
			{
				try
				{
					var image = CreateUIImage(cappedImage);

					var imageView = new UIImageView(image);

					SetNativeControl (imageView);
				}
				catch (Exception ex) {
					// Usually: Image not found
					Console.WriteLine ("CappedImageRenderer.OnElementChanged error: {0}", ex.Message);

					throw;
				}
			}
		}

		private UIImage CreateUIImage(CappedImage cappedImage) {
			Console.WriteLine ("Drawing Image '{0}', FlippedHorizontally={1}, FlippedVertically={2}", 
				cappedImage.ImageResource, cappedImage.FlippedHorizontally, cappedImage.FlippedVertically);

			var image = new UIImage (cappedImage.ImageResource);

			Thickness actualCapWidth = cappedImage.CapWidth;
			if (cappedImage.FlippedHorizontally || cappedImage.FlippedVertically) {
				var orientation = GetOrientation(cappedImage.FlippedHorizontally, cappedImage.FlippedVertically);
				image = new UIImage(image.CGImage, image.CurrentScale, orientation);
			
				if (cappedImage.HasCapWidth) {
					if (cappedImage.FlippedHorizontally) {
						var left = actualCapWidth.Left;
						actualCapWidth.Left = actualCapWidth.Right;
						actualCapWidth.Right = left;
					}
					if (cappedImage.FlippedVertically) {
						var top = actualCapWidth.Top;
						actualCapWidth.Top = actualCapWidth.Bottom;
						actualCapWidth.Bottom = top;
					}
				}
			}

			if (cappedImage.TintColor != Color.Default) {
				image = ImageEffects.ApplyTintEffect (image, cappedImage.TintColor.ToUIColor ());
			}

			if (cappedImage.HasCapWidth)
			{
				var capInsets = actualCapWidth.ToUIEdgeInsets();
				image = image.CreateResizableImage (capInsets);
			}

			return image;
		}

		private static UIImageOrientation GetOrientation(bool flippedHorizontally, bool flippedVertically) {
			if (flippedVertically) {
				if (flippedHorizontally) {
					return UIImageOrientation.Down; // Flipped horizontally and vertically is same as rotated 180 degrees
				}
				else
				{
					// Flipped vertically only
					return UIImageOrientation.DownMirrored;
				}
			}
			else if (flippedHorizontally) {
				// Flipped horizontally only
				return UIImageOrientation.UpMirrored;
			}

			return UIImageOrientation.Up; // Normal
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			// If any properties that affect the display are changed, we need to re-render
			if (e.PropertyName == CappedImage.CapWidthProperty.PropertyName ||
			        e.PropertyName == CappedImage.ImageResourceProperty.PropertyName ||
			        e.PropertyName == CappedImage.FlippedHorizontallyProperty.PropertyName ||
			        e.PropertyName == CappedImage.FlippedVerticallyProperty.PropertyName ||
					e.PropertyName == CappedImage.TintColorProperty.PropertyName) {


				var cappedImage = this.Element as CappedImage;
				if (cappedImage != null) {
					var image = CreateUIImage (cappedImage);
					var imageView = this.Control;
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

