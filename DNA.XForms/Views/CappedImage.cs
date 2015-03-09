using System;
using Xamarin.Forms;

namespace DNA.XForms
{
	/// <summary>
	/// An image which has a scalable region defined within it.
	/// In iOS, we specify the "caps" in code (the number of pixels on the top, left, right, and bottom that are NOT scaled)
	/// In Android, we use a special image format that contains a single-pixel black lineon each edge which defines the caps
	/// </summary>
	public class CappedImage : Image
	{
		#region BindableProperty Definitions

		public static readonly BindableProperty ImageResourceProperty = BindableProperty.Create<CappedImage, string>(p => p.ImageResource, "");
		public static readonly BindableProperty CapWidthProperty = BindableProperty.Create<CappedImage, Thickness>(p => p.CapWidth, new Thickness(-1d));
		public static readonly BindableProperty FlippedHorizontallyProperty = BindableProperty.Create<CappedImage, bool>(p => p.FlippedHorizontally, false);		
		public static readonly BindableProperty FlippedVerticallyProperty = BindableProperty.Create<CappedImage, bool>(p => p.FlippedVertically, false);
		public static readonly BindableProperty TintColorProperty = BindableProperty.Create<CappedImage, Color>(p => p.TintColor, Color.Default);

		public string ImageResource {
			get { return (string)base.GetValue (ImageResourceProperty); }
			private set { base.SetValue (ImageResourceProperty, value); }
		}

		// TODO: I'm not sure that Android supports setting this in code
		public Thickness CapWidth {
			get { return (Thickness)base.GetValue (CapWidthProperty); }
			private set { base.SetValue (CapWidthProperty, value); }
		}

		public bool FlippedHorizontally {
			get { return (bool)base.GetValue (FlippedHorizontallyProperty); }
			set { base.SetValue (FlippedHorizontallyProperty, value); }
		}

		public bool FlippedVertically {
			get { return (bool)base.GetValue (FlippedVerticallyProperty); }
			set { base.SetValue (FlippedVerticallyProperty, value); }
		}

		public Color TintColor {
			get { return (Color)base.GetValue (TintColorProperty); }
			set { base.SetValue (TintColorProperty, value); }
		}

		public bool HasCapWidth {
			get { return this.CapWidth != new Thickness (-1d);}
		}

		// TODO: Make this internal and set internals visible to DNA.XForms.iOS
		public bool LayoutPaused { get; private set;}

		#endregion

		public CappedImage (string imageResource) : this(imageResource, new Thickness(-1d))
		{
			this.ImageResource = imageResource;
		}
		public CappedImage (string imageResource, Thickness capWidth, bool flipHorizontally = false, bool flipVertically = false)
		{
			this.ImageResource = imageResource;
			this.CapWidth = capWidth;
			this.FlippedHorizontally = flipHorizontally;
			this.FlippedVertically = flipVertically;
		}

		public void SetImageAndCapWidth(string imageResource, Thickness capWidth)
		{
			if (capWidth == this.CapWidth) {
				// Only the image has changed - just go ahead and update it
				this.ImageResource = imageResource;
			} else {
				this.LayoutPaused = true;  // Suspending layout until both are set
				this.ImageResource = imageResource;  
				this.LayoutPaused = false;
				// CapWidth has changed, so this will cause it to re-render with the new image and cap width
				this.CapWidth = capWidth;
			}
		}
	}
}

