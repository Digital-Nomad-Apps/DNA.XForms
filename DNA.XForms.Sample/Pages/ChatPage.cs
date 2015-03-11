using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DNA.XForms.Sample
{
	public class ChatPage : ContentPage
	{
		public readonly List<string> SampleResponses;

		public ChatPage ()
		{
			this.SampleResponses = new List<string> {
				"Well, anything is possible.  Don't you think?",
				"Yes",
				"Maybe",
				"Are we still talking about that?",
				"Let's see how it unfolds",
				"Everything will be alright in the end, and if its not alright, then its not the end",
				"WTF!",
				"LOL",
				"Bacon",
				"The only way to play the game is not to play at all",
			};

			Title = "Chat Sample";

			// Our chat bubbles will get added to this stack layout
			var chatStackLayout = new StackLayout ();

			var textEntryPanel = new TextEntryPanel { EnterButtonText = "Say", Placeholder = "Have something to say?"};
			// textEntryPanel.Completed += {};  // TODO


			var mainLayout = new RelativeLayout ();
			// Scroll view fills up the entire space (including under the text input panel)
			mainLayout.Children.Add (new ScrollView { Content = chatStackLayout },
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent (parent => {
					return parent.Width;
				}),
				Constraint.RelativeToParent (parent => {
					return parent.Height;
				})
			);

			// Text input panel pinned to the bottom of the screen
			mainLayout.Children.Add (textEntryPanel,
				xConstraint: Constraint.Constant (0),
				yConstraint: Constraint.RelativeToParent (parent => { 
					System.Diagnostics.Debug.WriteLine ("Y constraint = {0}", parent.Height - textEntryPanel.Height);
					return parent.Height - textEntryPanel.Height; 
				}),
				widthConstraint: Constraint.RelativeToParent (parent => parent.Width)
			);

			this.Content = new ScrollView {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Content = mainLayout,
			};
		}
	}
}


