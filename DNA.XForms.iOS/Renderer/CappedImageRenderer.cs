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

					// Render something for debugging
					SetNativeControl (new UIImageView { BackgroundColor = Color.Red.ToUIColor() });
				}
			}
		}

		private UIImage CreateUIImage(CappedImage cappedImage) {
			return ImageHelper.CreateCappedUIImage (cappedImage.ImageResource, cappedImage.FlippedHorizontally, cappedImage.FlippedVertically, cappedImage.TintColor, cappedImage.CapWidth);
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
				
					if (cappedImage.LayoutPaused)
						return;

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

