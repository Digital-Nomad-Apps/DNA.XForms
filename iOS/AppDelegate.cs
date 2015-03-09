using System;
using System.Collections.Generic;
using System.Linq;

using DNA.XForms.Sample;

using Foundation;
using UIKit;

namespace DNA.XForms.Sample.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Ensure the custom Renderers get loaded
			DNA.XForms.iOS.Platform.Init ();

			LoadApplication (new App());

			return base.FinishedLaunching (app, options);
		}
	}
}

