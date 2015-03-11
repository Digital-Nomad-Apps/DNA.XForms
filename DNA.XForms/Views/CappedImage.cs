using Xamarin.Forms;

namespace DNA.XForms
{
	/// <summary>
	/// An image which has a scalable region defined within it.
	/// In iOS, we specify the "caps" in code (the number of pixels on the top, left, right, and bottom that are NOT scaled)
	/// In Android, we use a special image format that contains a single-pixel black lineon each edge which defines the caps
	/// </summary>
	/// <remarks>
	/// This is largely based on the work done here (just adapted for Xamarin.Forms)
	/// https://developer.xamarin.com/samples/monotouch/BubbleCell/
	/// </remarks>
	public class CappedImage : Image
	{ 
		#region BindableProperty Definitions

		public static readonly BindableProperty ImageResourceProperty = BindableProperty.Create<CappedImage, string>(p => p.ImageResource, "");
		public static readonly BindableProperty CapWidthProperty = BindableProperty.Create<CappedImage, Thickness>(p => p.CapWidth, new Thickness(-1d));
		public static readonly BindableProperty FlippedHorizontallyProperty = BindableProperty.Create<CappedImage, bool>(p => p.FlippedHorizontally, false);		
		public static readonly BindableProperty FlippedVerticallyProperty = BindableProperty.Create<CappedImage, bool>(p => p.FlippedVertically, false);
		public static readonly BindableProperty TintColorProperty = BindableProperty.Create<CappedImage, Color>(p => p.TintColor, Color.Default);
		public static readonly BindableProperty TintColorModeProperty = BindableProperty.Create<CappedImage, TintColorModes>(p => p.TintColorMode, TintColorModes.Solid);
		public static readonly BindableProperty HasShadowProperty = BindableProperty.Create<CappedImage, bool>(p => p.HasShadow, false);

		// Making this a bindable property will fire off the relevant events to the Renderer when layout is resumed
		public static readonly BindableProperty LayoutPausedProperty = BindableProperty.Create<CappedImage, bool>(p => p.LayoutPaused, false);

		public string ImageResource {
			get { return (string)base.GetValue (ImageResourceProperty); }
			set { base.SetValue (ImageResourceProperty, value); }
		}

		// TODO: I'm not sure that Android supports setting this in code
		public Thickness CapWidth {
			get { return (Thickness)base.GetValue (CapWidthProperty); }
			set { base.SetValue (CapWidthProperty, value); }
		}

		public bool FlippedHorizontally {
			get { return (bool)base.GetValue (FlippedHorizontallyProperty); }
			set { base.SetValue (FlippedHorizontallyProperty, value); }
		}

		public bool FlippedVertically {
			get { return (bool)base.GetValue (FlippedVerticallyProperty); }
			set { base.SetValue (FlippedVerticallyProperty, value); }
		}

		public bool HasShadow {
			get { return (bool)base.GetValue (HasShadowProperty); }
			set { base.SetValue (HasShadowProperty, value); }
		}

		public Color TintColor {
			get { return (Color)base.GetValue (TintColorProperty); }
			set { base.SetValue (TintColorProperty, value); }
		}

		public TintColorModes TintColorMode {
			get { return (TintColorModes)base.GetValue (TintColorModeProperty); }
			set { base.SetValue (TintColorModeProperty, value); }
		}

		public bool HasCapWidth {
			get { return this.CapWidth != new Thickness (-1d);}
		}
			
		public bool LayoutPaused { 
			get { return (bool)base.GetValue (LayoutPausedProperty); }
			set { base.SetValue (LayoutPausedProperty, value); }
		}

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
	}
}

