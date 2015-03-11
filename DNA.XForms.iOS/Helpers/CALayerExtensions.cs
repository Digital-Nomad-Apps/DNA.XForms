using System;
using UIKit;
using CoreAnimation;

namespace DNA.XForms.iOS
{
	public static class CALayerExtensions
	{
		public static void SetShadowPropertiesOnLayer(this CALayer layer, bool hasShadow) {
			if (hasShadow) {
				// TODO: Allow more customization of the shadow
				layer.ShadowColor = UIColor.Black.CGColor;
				layer.ShadowOffset = new System.Drawing.SizeF (0f, 0f);
				layer.ShadowOpacity = 0.8f;
				layer.ShadowRadius = 5f;
			} else {
				// Clear shadow
				layer.ShadowColor = UIColor.Clear.CGColor;
				layer.ShadowOffset = new System.Drawing.SizeF ();
				layer.ShadowOpacity = 0f;
				layer.ShadowRadius = 0f;
			}
		}
	}
}

