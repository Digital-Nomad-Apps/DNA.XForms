using System;
using UIKit;
using CoreGraphics;

namespace DNA.XForms.iOS.Views
{
	public partial class VectorSpeechBubbleUIView : UIView
	{
		public VectorSpeechBubble.ArrowDirections ArrowDirection { get; set;}
		public UIColor FillColor { get; set;}
		public UIColor GradientFillColor { get; set;}
		public UIColor BorderColor { get; set;}
		public float BorderWidth { get; set;}

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			DrawSpeechBubbleAroundRect (rect, this.ArrowDirection, this.BorderColor, this.FillColor, this.GradientFillColor, this.BorderWidth);
		}


		/// <summary>
		/// Create a vector-based speech bubble using Bezier Paths
		/// </summary>
		/// <returns>The speech bubble around rect.</returns>
		/// <param name="rect">Rect.</param>
		/// <param name="arrowDirection">Arrow can be on left or right and top or bottom</param>
		/// <param name = "borderColor"></param>
		/// <param name = "backgroundColor"></param>
		/// <param name = "strokeWidth"></param>
		/// <param name = "borderRadius"></param>
		/// <param name = "triangleHeight"></param>
		/// <param name = "triangleWidth"></param>
		public static void DrawSpeechBubbleAroundRect(CGRect rect, 
			VectorSpeechBubble.ArrowDirections arrowDirection, 
			UIColor borderColor = null, 
			UIColor fillColor = null, 
			UIColor gradientFillColor = null, 
			float borderWidth = 1f,
			float cornerRadius = 8f,
			float arrowHeight = 12f,
			float arrowWidth = 8f,
			float arrowOffset = 0f)
		{
			if (rect == CGRect.Empty)
				return; // Nothing to draw here folks

			bool useGradient = true;
			if (fillColor == gradientFillColor || gradientFillColor == null) {
				useGradient = false;
			}

			if (borderColor == null)
				borderColor = UIColor.Black;
			if (fillColor == null)
				fillColor = UIColor.LightGray;

			// Adjust the arrow offset to be within acceptable bounds
			if (arrowOffset < cornerRadius) {
				arrowOffset = cornerRadius;
				// don't start the arrow on the rounded part of the rounded rectangle
			}

			// Hide the arrow if there is not enough space to render it
			if (arrowDirection.IsLeft () || arrowDirection.IsRight ()) {
				if (arrowWidth > (rect.Width - cornerRadius))
					arrowDirection = VectorSpeechBubble.ArrowDirections.None;
			}
			if (arrowDirection.IsUp () || arrowDirection.IsDown ()) {
				if (arrowHeight > (rect.Height - cornerRadius))
					arrowDirection = VectorSpeechBubble.ArrowDirections.None;
			}

			using (var context = UIGraphics.GetCurrentContext ()) {
				context.SetLineJoin (CGLineJoin.Round);
				context.SetLineWidth (borderWidth);
				context.SetStrokeColor (borderColor.CGColor);	
				context.SetFillColor (fillColor.CGColor);

				var path = new CGPath ();
				path.MoveToPoint (cornerRadius + borderWidth + 0.5f, borderWidth + arrowHeight + 0.5f);
				path.AddLineToPoint ((nfloat)Math.Round(rect.Width / 2.0f - arrowWidth / 2.0f) + 0.5f, arrowHeight + borderWidth + 0.5f);
				path.AddLineToPoint ((nfloat)Math.Round(rect.Width / 2.0f) + 0.5f, borderWidth + 0.5f);
				path.AddLineToPoint ((nfloat)Math.Round(rect.Width / 2.0f + arrowWidth / 2.0f) + 0.5f, arrowHeight + borderWidth + 0.5f);
				path.AddArcToPoint (rect.Width - borderWidth - 0.5f, borderWidth + arrowHeight + 0.5f, rect.Width - borderWidth - 0.5f, rect.Height - borderWidth - 0.5f, cornerRadius - borderWidth);
				path.AddArcToPoint(rect.Width - borderWidth - 0.5f, rect.Height - borderWidth - 0.5f, (nfloat)Math.Round(rect.Width / 2.0f + arrowWidth / 2.0f) - borderWidth + 0.5f, rect.Height - borderWidth - 0.5f, cornerRadius - borderWidth);
				path.AddArcToPoint (borderWidth + 0.5f, rect.Height - borderWidth - 0.5f, borderWidth + 0.5f, arrowWidth + borderWidth + 0.5f, cornerRadius - borderWidth);
				path.AddArcToPoint(borderWidth + 0.5f, borderWidth + arrowHeight + 0.5f, rect.Width - borderWidth - 0.5f, arrowHeight + borderWidth + 0.5f, cornerRadius - borderWidth);
				path.CloseSubpath ();

				context.AddPath (path);

				if (useGradient) {

					context.Clip ();

					using (var rgb = CGColorSpace.CreateDeviceRGB ()) {
						CGGradient gradient = new CGGradient (rgb, new CGColor[] {
							gradientFillColor.CGColor,
							fillColor.CGColor,
						});
						
						context.DrawLinearGradient (gradient, 
							new CGPoint (path.BoundingBox.Left, path.BoundingBox.Top), 
							new CGPoint (path.BoundingBox.GetMidX (), path.BoundingBox.Bottom), 
							CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);

					}
						
					context.AddPath (path);
					context.DrawPath (CGPathDrawingMode.Stroke);
				} else {
					// Single color only 
					context.DrawPath (CGPathDrawingMode.FillStroke);
				}
			}
		}

		/*
			 * Original Objective-C version from http://stackoverflow.com/questions/4442126/how-to-draw-a-speech-bubble-on-an-iphone
			CGRect currentFrame = self.bounds;

			CGContextSetLineJoin(context, kCGLineJoinRound);
			CGContextSetLineWidth(context, strokeWidth);
			CGContextSetStrokeColorWithColor(context, [MyPopupLayer popupBorderColor]); 
			CGContextSetFillColorWithColor(context, [MyPopupLayer popupBackgroundColor]);

			// Draw and fill the bubble
			CGContextBeginPath(context);
			CGContextMoveToPoint(context, borderRadius + strokeWidth + 0.5f, strokeWidth + HEIGHTOFPOPUPTRIANGLE + 0.5f);
			CGContextAddLineToPoint(context, round(currentFrame.size.width / 2.0f - WIDTHOFPOPUPTRIANGLE / 2.0f) + 0.5f, HEIGHTOFPOPUPTRIANGLE + strokeWidth + 0.5f);
			CGContextAddLineToPoint(context, round(currentFrame.size.width / 2.0f) + 0.5f, strokeWidth + 0.5f);
			CGContextAddLineToPoint(context, round(currentFrame.size.width / 2.0f + WIDTHOFPOPUPTRIANGLE / 2.0f) + 0.5f, HEIGHTOFPOPUPTRIANGLE + strokeWidth + 0.5f);
			CGContextAddArcToPoint(context, currentFrame.size.width - strokeWidth - 0.5f, strokeWidth + HEIGHTOFPOPUPTRIANGLE + 0.5f, currentFrame.size.width - strokeWidth - 0.5f, currentFrame.size.height - strokeWidth - 0.5f, borderRadius - strokeWidth);
			CGContextAddArcToPoint(context, currentFrame.size.width - strokeWidth - 0.5f, currentFrame.size.height - strokeWidth - 0.5f, round(currentFrame.size.width / 2.0f + WIDTHOFPOPUPTRIANGLE / 2.0f) - strokeWidth + 0.5f, currentFrame.size.height - strokeWidth - 0.5f, borderRadius - strokeWidth);
			CGContextAddArcToPoint(context, strokeWidth + 0.5f, currentFrame.size.height - strokeWidth - 0.5f, strokeWidth + 0.5f, HEIGHTOFPOPUPTRIANGLE + strokeWidth + 0.5f, borderRadius - strokeWidth);
			CGContextAddArcToPoint(context, strokeWidth + 0.5f, strokeWidth + HEIGHTOFPOPUPTRIANGLE + 0.5f, currentFrame.size.width - strokeWidth - 0.5f, HEIGHTOFPOPUPTRIANGLE + strokeWidth + 0.5f, borderRadius - strokeWidth);
			CGContextClosePath(context);
			CGContextDrawPath(context, kCGPathFillStroke);

			// Draw a clipping path for the fill
			CGContextBeginPath(context);
			CGContextMoveToPoint(context, borderRadius + strokeWidth + 0.5f, round((currentFrame.size.height + HEIGHTOFPOPUPTRIANGLE) * 0.50f) + 0.5f);
			CGContextAddArcToPoint(context, currentFrame.size.width - strokeWidth - 0.5f, round((currentFrame.size.height + HEIGHTOFPOPUPTRIANGLE) * 0.50f) + 0.5f, currentFrame.size.width - strokeWidth - 0.5f, currentFrame.size.height - strokeWidth - 0.5f, borderRadius - strokeWidth);
			CGContextAddArcToPoint(context, currentFrame.size.width - strokeWidth - 0.5f, currentFrame.size.height - strokeWidth - 0.5f, round(currentFrame.size.width / 2.0f + WIDTHOFPOPUPTRIANGLE / 2.0f) - strokeWidth + 0.5f, currentFrame.size.height - strokeWidth - 0.5f, borderRadius - strokeWidth);
			CGContextAddArcToPoint(context, strokeWidth + 0.5f, currentFrame.size.height - strokeWidth - 0.5f, strokeWidth + 0.5f, HEIGHTOFPOPUPTRIANGLE + strokeWidth + 0.5f, borderRadius - strokeWidth);
			CGContextAddArcToPoint(context, strokeWidth + 0.5f, round((currentFrame.size.height + HEIGHTOFPOPUPTRIANGLE) * 0.50f) + 0.5f, currentFrame.size.width - strokeWidth - 0.5f, round((currentFrame.size.height + HEIGHTOFPOPUPTRIANGLE) * 0.50f) + 0.5f, borderRadius - strokeWidth);
			CGContextClosePath(context);
			CGContextClip(context); 
			*/
	}
}

