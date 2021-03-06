﻿using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System;
using UIKit;
using CoreGraphics;

namespace DNA.XForms.iOS
{
	public static class ImageHelper
	{
		public static UIImage CreateCappedUIImage(string imageResource, bool flipHorizontally, bool flipVertically, Color tintColor, TintColorModes mode, Thickness capWidth) {
			Console.WriteLine ("Creating UIImage '{0}', FlipHorizontally={1}, FlipVertically={2}",
				imageResource, flipHorizontally, flipVertically);

			if (tintColor != Color.Default)
				Console.WriteLine ("   TintColor={0}", tintColor);

			if (capWidth.IsNotSet()) {
				Console.WriteLine ("   Cap.Left={0}, Cap.Top={1}, Cap.Right={2}, Cap.Bottom={3}",
					capWidth.Left, capWidth.Top, capWidth.Right, capWidth.Bottom);
			}

			var image = new UIImage (imageResource);

			Thickness adjustedCapWidth = capWidth;
			if (flipHorizontally || flipVertically) {
				var orientation = GetOrientation(flipHorizontally, flipVertically);
				image = new UIImage(image.CGImage, image.CurrentScale, orientation);

				if (!capWidth.IsNotSet()) {
					if (flipHorizontally) {
						var left = adjustedCapWidth.Left;
						adjustedCapWidth.Left = adjustedCapWidth.Right;
						adjustedCapWidth.Right = left;
					}
					if (flipVertically) {
						var top = adjustedCapWidth.Top;
						adjustedCapWidth.Top = adjustedCapWidth.Bottom;
						adjustedCapWidth.Bottom = top;
					}
				}
			}

			if (tintColor != Color.Default) {
				image = ApplyTintEffect (image, tintColor.ToUIColor (), mode);
			}

			if (!capWidth.IsNotSet())
			{
				var capInsets = adjustedCapWidth.ToUIEdgeInsets();
				image = image.CreateResizableImage (capInsets, UIImageResizingMode.Stretch);
			}

			return image;
		}

		public static UIImage ApplyTintEffect (UIImage image, UIColor color, TintColorModes mode)
		{
			UIGraphics.BeginImageContextWithOptions (image.Size, false, image.CurrentScale);

			try
			{
				UIImage result;
				using (CGContext g = UIGraphics.GetCurrentContext ()) {
					var rect = new CGRect (location:CGPoint.Empty, size:image.Size);

					if (mode == TintColorModes.Solid) {
						image.Draw(rect);
						g.SetFillColor (color.CGColor);
						g.SetBlendMode(CGBlendMode.SourceAtop);
						g.FillRect(rect);
					}
					else if (mode == TintColorModes.Gradient) 
					{
						g.SetBlendMode(CGBlendMode.Normal);
						g.SetFillColor(UIColor.Black.CGColor);
						g.FillRect(rect);

						// draw original image
						// g.SetBlendMode(CGBlendMode.Normal);
						// g.DrawImage(rect, image.CGImage);
						image.Draw(rect, CGBlendMode.Normal, 1f);

						// tint image (losing alpha) - the luminosity of the original image is preserved
						g.SetBlendMode(CGBlendMode.Color);
						g.SetFillColor(color.CGColor);
						g.FillRect(rect);

						// mask by alpha values of original image
						// g.SetBlendMode(CGBlendMode.DestinationIn);
						// g.DrawImage(rect, image.CGImage);
						image.Draw(rect, CGBlendMode.DestinationIn, 1f);
					}

					result = UIGraphics.GetImageFromCurrentImageContext();
				}
				return result;
			}
			finally {
				UIGraphics.EndImageContext();
			}
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
	}
}

