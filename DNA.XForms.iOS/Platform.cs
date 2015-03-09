using System;

namespace DNA.XForms.iOS
{
	public static class Platform
	{
		public static void Init ()
		{
			// This doesn't do anything, but calling this method ensures that the renderers get loaded
			Console.WriteLine ("DNA.XForms.iOS.Platform: Init() called");
		}
	}
}

