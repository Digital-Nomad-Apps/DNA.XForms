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
		private readonly Color DefaultColor = Color.White;
		private UIView childView;

		protected override void OnElementChanged (ElementChangedEventArgs<RoundedFrame> e)
		{
			base.OnElementChanged (e);

			var roundedFrame = e.NewElement;
			if (roundedFrame != null) {
				var frameUIView = CreateFrameUIView (roundedFrame);
				SetNativeControl (frameUIView);

				SetBackgroundColorWithDefault (roundedFrame.BackgroundColor, DefaultColor);

				this.SetBackgroundColor (Color.Transparent);
				this.BackgroundColor = Color.Transparent.ToUIColor();
			}
		}

		private void SetBackgroundColorWithDefault(Color color, Color defaultColor) {
			if (color == Color.Default) {
				childView.BackgroundColor = defaultColor.ToUIColor();
			} else {
				childView.BackgroundColor = color.ToUIColor ();
			}
			this.SetBackgroundColor (Color.Transparent);
			this.BackgroundColor = Color.Transparent.ToUIColor();
		}

		private UIView CreateFrameUIView(RoundedFrame roundedFrame) {
			// Shadows on rounded rect require 2 views, one for the rounded rect, and one for the shadow
			childView = new UIView () {
				Layer = {
					CornerRadius = (System.nfloat)roundedFrame.CornerRadius,
					BorderColor = roundedFrame.OutlineColor.ToCGColor (),
					BorderWidth = (System.nfloat)roundedFrame.OutlineWidth,
				},
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight,
			};

			var shadowView = new UIView ();
			shadowView.Add (childView);
			shadowView.Layer.SetShadowPropertiesOnLayer(roundedFrame.HasShadow);

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

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			var roundedFrameElement = this.Element as RoundedFrame;
			if (roundedFrameElement != null) {
				// Respond to changes in properties that may affect the renderering
				if (e.PropertyName == RoundedFrame.CornerRadiusProperty.PropertyName)
					childView.Layer.CornerRadius = (float)this.Element.CornerRadius;
				else if (e.PropertyName == RoundedFrame.OutlineColorProperty.PropertyName)
					childView.Layer.BorderColor = roundedFrameElement.OutlineColor.ToCGColor ();
				// else if (e.PropertyName == Frame.BorderWidthProperty.PropertyName)
				// 	childView.Layer.BorderWidth = (float)this.Element.BorderWidth;
				else if (e.PropertyName == RoundedFrame.BackgroundColorProperty.PropertyName) {
					childView.BackgroundColor = roundedFrameElement.BackgroundColor.ToUIColor ();
					SetBackgroundColorWithDefault (roundedFrameElement.BackgroundColor, DefaultColor);
				} else if (e.PropertyName == RoundedFrame.OutlineWidthProperty.PropertyName) {
					childView.Layer.BorderWidth = (System.nfloat)roundedFrameElement.OutlineWidth;
				}
				if (e.PropertyName == RoundedFrame.HasShadowProperty.PropertyName)
					this.Control.Layer.SetShadowPropertiesOnLayer(this.Element.HasShadow);
			}
		}
	}
}
