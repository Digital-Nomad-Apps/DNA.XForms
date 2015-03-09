using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using DNA.XForms;


[assembly: ExportRenderer(typeof(RoundedFrame), typeof(DNA.XForms.iOS.Renderer.RoundedFrameRenderer))]

namespace DNA.XForms.iOS.Renderer
{
	/// <summary>
	/// Rounded frame renderer
	/// </summary>
	public class RoundedFrameRenderer : ViewRenderer<RoundedFrame, UIView>
	{
		private UIView childView;

		protected override void OnElementChanged (ElementChangedEventArgs<RoundedFrame> e)
		{
			base.OnElementChanged (e);

			var roundedFrame = e.NewElement;
			if (roundedFrame != null) {
				this.SetBackgroundColor (Color.Transparent);

				var frameUIView = CreateFrameUIView (roundedFrame);
				SetNativeControl (frameUIView);
			}
		}

		private UIView CreateFrameUIView(RoundedFrame roundedFrame) {

			this.SetBackgroundColor(Color.Transparent); // this does nothing
			this.BackgroundColor = Color.Transparent.ToUIColor();

			// Shadows on rounded rect require 2 views, one for the rounded rect, and one for the shadow
			childView = new UIView () {
				BackgroundColor = roundedFrame.BackgroundColor.ToUIColor (),
				Layer = {
					CornerRadius = (float)roundedFrame.CornerRadius,
					BorderColor = roundedFrame.OutlineColor.ToCGColor (),
					BorderWidth = 1f, // (float)roundedFrame.OutlineWidth, // TODO
				},
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight,
			};

			var shadowView = new UIView ();
			shadowView.Add (childView);
			SetShadowPropertiesOnLayer (shadowView.Layer, roundedFrame.HasShadow);

			// Frame should be behind any of the Content views // MO: I'm not sure if this is working
			this.SendSubviewToBack(shadowView);

			// http://forums.xamarin.com/discussion/30513/how-to-add-the-content-of-a-frame-as-a-subview-in-an-uiview-add-views-as-subviews-of-uiview
			// BUT: does this end up rendering twice?
			if (roundedFrame.Content != null) {
				var rend = RendererFactory.GetRenderer (roundedFrame.Content);
				shadowView.Add(rend.NativeView);
			}

			return shadowView;
		}


		private void SetShadowPropertiesOnLayer(CoreAnimation.CALayer layer, bool hasShadow) {
			if (hasShadow) {
				// TODO: Allow more customization of the shadow
				layer.ShadowColor = UIColor.Black.CGColor;
				layer.ShadowOffset = new System.Drawing.SizeF (3f, 3f);
				layer.ShadowOpacity = 1f;
				layer.ShadowRadius = 5f;
			} else {
				layer.ShadowColor = UIColor.Clear.CGColor;
				layer.ShadowOffset = new System.Drawing.SizeF ();
				layer.ShadowOpacity = 0f;
				layer.ShadowRadius = 0f;
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var roundedFrameElement = this.Element as RoundedFrame;
			if (roundedFrameElement != null) {
				// Respond to changes in properties that may affect the renderering
				if (e.PropertyName == RoundedFrame.CornerRadiusProperty.PropertyName)
					childView.Layer.CornerRadius = (float)this.Element.CornerRadius;
				else if (e.PropertyName == RoundedFrame.OutlineColorProperty.PropertyName)
					childView.Layer.BorderColor =roundedFrameElement.OutlineColor.ToCGColor ();
				// else if (e.PropertyName == Frame.BorderWidthProperty.PropertyName)
				// 	childView.Layer.BorderWidth = (float)this.Element.BorderWidth;

				if (e.PropertyName == RoundedFrame.HasShadowProperty.PropertyName)
					SetShadowPropertiesOnLayer (this.Control.Layer, this.Element.HasShadow);

				// TODO: SetsNeedsDisplay required?
			}
		}
	}
}

