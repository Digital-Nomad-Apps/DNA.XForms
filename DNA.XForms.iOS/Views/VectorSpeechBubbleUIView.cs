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
			UIColor borderColor, 
			UIColor fillColor, 
			UIColor gradientFillColor, 
			float borderWidth,
			float cornerRadius = 16f,
			float arrowHeight = 16f,
			float arrowWidth = 12f,
			float arrowOffset = 0f)
		{
			if (rect == CGRect.Empty)
				return; // Nothing to draw here folks

			Console.WriteLine("VectorSpeechBubbleUIView: DrawSpeechBubbleAroundRect");

			bool useGradient = true;
			if (fillColor == gradientFillColor || gradientFillColor == null) {
				useGradient = false;
			}

			// Adjust the arrow offset to be within acceptable bounds
			if (arrowOffset < cornerRadius) {
				arrowOffset = cornerRadius;
				// don't start the arrow on the rounded part of the rounded rectangle
			}

			// Reduce the border width if it is bigger than the width or the height of the control
			// Strange things happen at the edge :)
			if (borderWidth > (rect.Width / 2f))
				borderWidth = (float)(rect.Width / 2f);
			if (borderWidth > (rect.Height / 2f))
				borderWidth = (float)(rect.Height / 2f);

			// Reduce the arrow width if there is not enough space to render it
			if (arrowDirection.IsLeft () || arrowDirection.IsRight ()) {
				if (arrowWidth > rect.Height)
					arrowWidth = (float)rect.Height;
			}
			if (arrowDirection.IsUp () || arrowDirection.IsDown ()) {
				if (arrowHeight > rect.Width)
					arrowWidth = (float)rect.Width;
			}

			using (var context = UIGraphics.GetCurrentContext ()) {
				context.SetLineJoin (CGLineJoin.Round);
				context.SetLineWidth (borderWidth);
				context.SetStrokeColor (borderColor.CGColor);	
				context.SetFillColor (fillColor.CGColor);

				bool hasUpArrow = arrowDirection.IsUp();
				bool hasDownArrow = arrowDirection.IsDown();
				bool hasLeftArrow = arrowDirection.IsLeft();
				bool hasRightArrow = arrowDirection.IsRight();

				context.SetLineJoin (CGLineJoin.Round);
				context.SetLineWidth (borderWidth);
				context.SetStrokeColor (borderColor.CGColor);	
				context.SetFillColor (fillColor.CGColor);

				var path = new CGPath ();

				var midArrowWidth = arrowWidth / 2.0f;

				var leftX = borderWidth + 0.5f;
				if (hasLeftArrow)
					leftX = leftX + arrowHeight;

				var rightX = rect.Width - borderWidth - 0.5f;
				if (hasRightArrow)
					rightX = rightX - arrowHeight;

				var topY = borderWidth + 0.5f;
				if (hasUpArrow) {
					topY = topY + arrowHeight;
				}

				var bottomY = rect.Height - borderWidth - 0.5f;
				if (hasDownArrow) {
					bottomY = bottomY - arrowHeight;
				}

				var midY = (nfloat)Math.Round ((topY + bottomY) / 2.0f) + 0.5f;
				var midX = (nfloat)Math.Round ((leftX + rightX) / 2.0f) + 0.5f;

				var effectiveCornerRadius = cornerRadius - borderWidth;
				if (effectiveCornerRadius < 0f) {
					effectiveCornerRadius = 0f;
				}

				// Starts at top left corner
				path.MoveToPoint (leftX + effectiveCornerRadius, topY);

				if (hasUpArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint ((nfloat)Math.Round(midX - midArrowWidth) + 0.5f, topY);
					// Draws the arrow up, and then down again
					path.AddLineToPoint (midX, borderWidth + 0.5f);
					path.AddLineToPoint ((midX + midArrowWidth) + 0.5f, arrowHeight + borderWidth + 0.5f);
				}

				// Top right corner
				path.AddArcToPoint (rightX, topY, rightX, bottomY, effectiveCornerRadius);

				if (hasRightArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint (rightX, midY - midArrowWidth );
					// Draws the arrow right, and then left again
					path.AddLineToPoint (rightX + arrowHeight, midY);
					path.AddLineToPoint (rightX, midY + midArrowWidth);
				}

				// To Bottom right corner (curling towards bottom left corner)
				path.AddArcToPoint(rightX, bottomY, leftX, bottomY, effectiveCornerRadius);

				if (hasDownArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint (midX + midArrowWidth, bottomY);

					// Draws the arrow up, and then down again
					path.AddLineToPoint (midX, bottomY + arrowHeight);
					path.AddLineToPoint (midX - midArrowWidth, bottomY);
				}

				// To bottom left corner (curling up in direction of top left corner)
				path.AddArcToPoint (leftX, bottomY, leftX, topY, effectiveCornerRadius);

				if (hasLeftArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint (leftX, midY + midArrowWidth );
					// Draws the arrow right, and then left again
					path.AddLineToPoint (leftX - arrowHeight, midY);
					path.AddLineToPoint (leftX, midY - midArrowWidth);
				}

				// To top left corner (curling in direction of top right corner)
				path.AddArcToPoint(leftX, topY, rightX, topY, effectiveCornerRadius);



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

