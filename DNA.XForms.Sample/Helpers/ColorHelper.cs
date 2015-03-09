using System;

namespace DNA.XForms.Sample
{
	public struct ColorHelper
	{
		public static readonly ColorHelper MyBlue = 0x59759E;		//  Color.FromRgb (89, 117, 158); 
		public static readonly ColorHelper MyLightBlue = 0x6fa8dc;	//  Color.FromRgb (89, 117, 158); 

	
		// Colors for specific purposes
		public static readonly Xamarin.Forms.Color ToolbarBackground = MyBlue.ToFormsColor();
		public static readonly Xamarin.Forms.Color MenuBackgroundColor = MyBlue.ToFormsColor();
		public static readonly Xamarin.Forms.Color SelectedMenuItemHighlight = MyLightBlue.ToFormsColor();

		public double R, G, B;
		public static ColorHelper FromHex(int hex)
		{
			Func<int, int> at = offset => (hex >> offset) & 0xFF;
			return new ColorHelper
			{
				R = at(16) / 255.0,
				G = at(8) / 255.0,
				B = at(0) / 255.0
			};
		}

		public static implicit operator ColorHelper(int hex)
		{
			return FromHex(hex);
		}

		public Xamarin.Forms.Color ToFormsColor()
		{
			return Xamarin.Forms.Color.FromRgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}
	}
}

