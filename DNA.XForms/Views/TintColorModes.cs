namespace DNA.XForms
{
	public enum TintColorModes
	{
		None = 0,
		Solid = 1,		// All opaque pixels are tinted to the TintColor, while transparent or semi-transparent pixels are preserved 
		Gradient = 2,	// The tint preserves gradient effects as well as transparency.  Semi-transparent pixels are not tinted
	}
}

