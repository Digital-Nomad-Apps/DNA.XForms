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
		public float CornerRadius {get;set;}

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			DrawSpeechBubbleAroundRect (rect, this.ArrowDirection, this.BorderColor, this.FillColor, this.GradientFillColor, this.BorderWidth, this.CornerRadius);
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
			float cornerRadius,
			float arrowHeight = 16f,
			float arrowWidth = 12f,
			float arrowOffset = 0f)
		{
			if (rect == CGRect.Empty)
				return; // Nothing to draw here folks

			Console.WriteLine("VectorSpeechBubbleUIView: DrawSpeechBubbleAroundRect");

			bool hasUpArrow = arrowDirection.IsUp();
			bool hasDownArrow = arrowDirection.IsDown();
			bool hasLeftArrow = arrowDirection.IsLeft();
			bool hasRightArrow = arrowDirection.IsRight();

			float boxWidth = (float)rect.Width - (hasLeftArrow ? arrowHeight : 0f) - (hasRightArrow ? arrowHeight : 0f); 
			float boxHeight = (float)rect.Height - (hasUpArrow ? arrowHeight : 0f) - (hasDownArrow ? arrowHeight : 0f); 

			if (boxWidth < 0f) {
				System.Diagnostics.Debug.Fail("Not enough space for the left and/or right arrows: funky rendering is likely!");
			}
			if (boxHeight < 0f) {
				System.Diagnostics.Debug.Fail("Not enough space for the top and/or bottom arrows: funky rendering is likely!");
			}

			bool useGradient = true;
			if (fillColor == gradientFillColor || gradientFillColor == null) {
				useGradient = false;
			}

			// Adjust the corner radius to prevent strange things happening when the width/height of the control is very small
			if (cornerRadius > (boxWidth - arrowWidth)/2f) {
				cornerRadius = (boxWidth - arrowWidth)/2f;
			}
			if (cornerRadius > (boxHeight - arrowWidth)/2f) {
				cornerRadius = (boxHeight - arrowWidth)/2f;
			}

			// Adjust the arrow offset to be within acceptable bounds
			if (arrowOffset < cornerRadius) {
				arrowOffset = cornerRadius;
				// don't start the arrow on the rounded part of the rounded rectangle
			}

			// Reduce the border width if it is bigger than the width or the height of the control
			// Strange things happen at the edge :)
			if (borderWidth > rect.Width)
				borderWidth = (float)rect.Width;
			if (borderWidth > rect.Height)
				borderWidth = (float)rect.Height;

			// Reduce the arrow width if there is not enough space to render it
			// TODO: This doesn't seem to have any effect
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

				var path = new CGPath ();

				var midArrowWidth = arrowWidth / 2.0f;

				var leftX = 0.5f;
				if (hasLeftArrow)
					leftX = leftX + arrowHeight;

				float rightX = (float)Math.Round(rect.Width) - 0.5f;
				if (hasRightArrow)
					rightX = rightX - arrowHeight;

				var topY = 0.5f;
				if (hasUpArrow) {
					topY = topY + arrowHeight;
				}

				float bottomY = (float)rect.Height - 0.5f;
				if (hasDownArrow) {
					bottomY = bottomY - arrowHeight;
				}

				var midY = (nfloat)Math.Round ((topY + bottomY) / 2.0f) + 0.5f;
				var midX = (nfloat)Math.Round ((leftX + rightX) / 2.0f) + 0.5f;

				// Starts at top left corner
				path.MoveToPoint (leftX + cornerRadius, topY);

				// Determine offset for the arrow position based on whether its left, center, or right, and the ArrowOffset property
				nfloat arrowOffsetX = 0f;
				nfloat arrowOffsetY = 0f;

				if (arrowDirection == VectorSpeechBubble.ArrowDirections.UpLeft || arrowDirection == VectorSpeechBubble.ArrowDirections.DownLeft) {
					arrowOffsetX = (nfloat)(-1 * ((rect.Width / 2f) - midArrowWidth - cornerRadius - 4f)); // Move the up/down arrow to the left corner
					// Items on the left shouldn't be right of the midline
					if (arrowOffsetX > 0f)
						arrowOffsetX = 0f;
				}
				if (arrowDirection == VectorSpeechBubble.ArrowDirections.UpRight || arrowDirection == VectorSpeechBubble.ArrowDirections.DownRight) {
					arrowOffsetX = (nfloat)((rect.Width / 2f) - midArrowWidth - cornerRadius - 4f); 		// Move the up/down arrow to the right corner
					// Items on the right shouldn't be left of the midline
					if (arrowOffsetX < 0f)
						arrowOffsetX = 0f;
				}
				if (arrowDirection == VectorSpeechBubble.ArrowDirections.LeftTop || arrowDirection == VectorSpeechBubble.ArrowDirections.RightTop) {
					arrowOffsetY = (nfloat)(-1 * ((rect.Height / 2f) - midArrowWidth - cornerRadius - 4f)); // Move the left/tight arrow to the top corner
					// Items on the top shouldn't be below the midline
					if (arrowOffsetY > 0f)
						arrowOffsetY = 0f;
				}
				if (arrowDirection == VectorSpeechBubble.ArrowDirections.LeftBottom || arrowDirection == VectorSpeechBubble.ArrowDirections.RightBottom) {
					arrowOffsetY = (nfloat)((rect.Height / 2f) - midArrowWidth - cornerRadius - 4f); 		// Move the up/down arrow to the bottom corner
					// Items on the bottom shouldn't be above the midline
					if (arrowOffsetY < 0f)
						arrowOffsetY = 0f;
				}

				if (hasUpArrow) {

					// Adds a line to where the arrow starts
					path.AddLineToPoint ((nfloat)Math.Round(midX - midArrowWidth) + 0.5f + arrowOffsetX, topY);
					// Draws the arrow up, and then down again
					path.AddLineToPoint (midX + arrowOffsetX,  0.5f);
					path.AddLineToPoint (midX + midArrowWidth + 0.5f + arrowOffsetX, arrowHeight + 0.5f);
				}

				// Top right corner
				path.AddArcToPoint (rightX, topY, rightX, bottomY, cornerRadius);

				if (hasRightArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint (rightX, midY - midArrowWidth + arrowOffsetY );
					// Draws the arrow right, and then left again
					path.AddLineToPoint (rightX + arrowHeight, midY + arrowOffsetY);
					path.AddLineToPoint (rightX, midY + midArrowWidth + arrowOffsetY);
				}

				// To Bottom right corner (curling towards bottom left corner)
				path.AddArcToPoint(rightX, bottomY, leftX, bottomY, cornerRadius);

				if (hasDownArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint (midX + midArrowWidth + arrowOffsetX, bottomY);

					// Draws the arrow up, and then down again
					path.AddLineToPoint (midX + arrowOffsetX, bottomY + arrowHeight);
					path.AddLineToPoint (midX - midArrowWidth + arrowOffsetX, bottomY);
				}

				// To bottom left corner (curling up in direction of top left corner)
				path.AddArcToPoint (leftX, bottomY, leftX, topY, cornerRadius);

				if (hasLeftArrow) {
					// Adds a line to where the arrow starts
					path.AddLineToPoint (leftX, midY + midArrowWidth + arrowOffsetY );
					// Draws the arrow right, and then left again
					path.AddLineToPoint (leftX - arrowHeight, midY + arrowOffsetY);
					path.AddLineToPoint (leftX, midY - midArrowWidth + arrowOffsetY);
				}

				// To top left corner (curling in direction of top right corner)
				path.AddArcToPoint(leftX, topY, rightX, topY, cornerRadius);

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
					context.Clip ();
					context.AddPath (path);

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

