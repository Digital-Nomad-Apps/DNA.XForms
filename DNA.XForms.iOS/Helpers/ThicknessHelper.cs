using System;
using UIKit;
using Xamarin.Forms;

namespace DNA.XForms.iOS
{
	public static class ThicknessHelper
	{
		public static UIEdgeInsets ToUIEdgeInsets(this Thickness sourceThickness) {
			return new UIEdgeInsets ((nfloat)sourceThickness.Top, (nfloat)sourceThickness.Left, (nfloat)sourceThickness.Bottom, (nfloat)sourceThickness.Right);
		}

		public static bool IsNotSet(this Thickness sourceThickness) {
			return sourceThickness == new Thickness (-1d); // Special constant value to indicate NotSet
		}
	}
}

