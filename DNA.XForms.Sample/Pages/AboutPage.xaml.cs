using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DNA.XForms.Sample
{
	public partial class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			InitializeComponent ();

			// Workaround for the fact that labels have no Clicked event
			var dnaTGR = new TapGestureRecognizer ();
			dnaTGR.Tapped += OnDnaLogoTapped;
			this.DnaLabel.GestureRecognizers.Add (dnaTGR);

			var twitterTGR = new TapGestureRecognizer ();
			twitterTGR.Tapped += OnTwitterTapped;
			this.TwitterLabel.GestureRecognizers.Add (twitterTGR);

			var linkedInTGR = new TapGestureRecognizer ();
			linkedInTGR.Tapped += OnLinkedInTapped;
			this.LinkedInLabel.GestureRecognizers.Add (linkedInTGR);
		}			

		protected async void OnDnaLogoTapped (object sender, EventArgs e)
		{
			var action = await DisplayActionSheet ("Launch Digital Nomad Apps Website?", "Cancel", null, "Open");
			if (action == "Open") {
				Device.OpenUri (new Uri("http://www.digitalnomadapps.co/"));
			}
		}

		protected async void OnTwitterTapped(object sender, EventArgs e)
		{
			var action = await DisplayActionSheet ("Launch Twitter Website?", "Cancel", null, "Open");
			if (action == "Open") {
				Device.OpenUri (new Uri("https://twitter.com/teevus"));
			}
		}

		protected async void OnLinkedInTapped(object sender, EventArgs e)
		{
			var action = await DisplayActionSheet ("Launch LinkedIn Website?", "Cancel", null, "Open");
			if (action == "Open") {
				Device.OpenUri (new Uri("https://au.linkedin.com/in/mattdna"));
			}
		}
	}
}

