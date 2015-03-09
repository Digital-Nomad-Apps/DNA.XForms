using System;
using UIKit;
using CoreGraphics;

namespace DNA.XForms.iOS
{
	public static class ImageEffects
	{
		public static UIImage ApplyTintEffect (UIImage image, UIColor color)
		{
			UIGraphics.BeginImageContextWithOptions (image.Size, false, image.CurrentScale);

			try
			{
				UIImage result;
				using (CGContext g = UIGraphics.GetCurrentContext ()) {
					var rect = new CGRect (location:CGPoint.Empty, size:image.Size);

					image.Draw(rect);
					// g.DrawImage(rect, image);

					g.SetFillColor (color.CGColor);
					g.SetBlendMode(CGBlendMode.SourceAtop);
					g.FillRect(rect);

					result = UIGraphics.GetImageFromCurrentImageContext();
				}
				return result;
			}
			finally {
				UIGraphics.EndImageContext();
			}

		}
	}
}

