using System;
using System.Linq;
using Xamarin.Forms;
using DNA.XForms.Sample.ViewModels;
using MenuItem = DNA.XForms.Sample.ViewModels.MenuItem;

namespace DNA.XForms.Sample.Pages
{
	/// <summary>
	/// The Root page is a container for the slide-out menu from the left and the "content" view on the right
	/// </summary>
	public class RootPage : MasterDetailPage
	{
		MenuItem previousItem;

		public RootPage ()
		{
			var menuPage = new MenuPage {Title = "menu", };
			Device.OnPlatform (iOS: () => {
				menuPage.Icon = "menu_icon.png";	// Don;t do this on Android as it uses the Application icon in the navigation bar
			});


			menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

			this.Master = menuPage;

			NavigateTo(menuPage.Menu.ItemsSource.Cast<MenuItem>().First());
		}

		/// <summary>
		/// Navigate to the view for the specified menu option
		/// </summary>
		public void NavigateTo(DNA.XForms.Sample.ViewModels.MenuItem option) {
			if (previousItem != null)
				previousItem.Selected = false;

			option.Selected = true;
			previousItem = option;

			var displayPage = PageForOption(option);

			Device.OnPlatform (WinPhone: () => Detail = new ContentPage ());  //work around to clear current page.

			Detail = new NavigationPage(displayPage)
			{
				BarBackgroundColor = ColorHelper.ToolbarBackground,
				BarTextColor = Color.White,
			};

			IsPresented = false;
		}

		Page PageForOption (MenuItem option)
		{
			if (option is AboutMenuItem)
				return new AboutPage ();
			if (option is SpeechBubbleMenuItem)
				return new SpeechBubblePage ();
			if (option is CappedImageMenuItem)
				return new CappedImagePage ();
			if (option is RoundedFrameMenuItem)
				return new RoundedFramePage ();

			throw new NotImplementedException("Unknown menu option: " + option.Title);
		}
	}
}

