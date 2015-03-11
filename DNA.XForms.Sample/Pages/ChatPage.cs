using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace DNA.XForms.Sample
{
	public class ChatPage : ContentPage
	{
		private Random _random = new Random ();
		private VectorSpeechBubble _lastBubble = null;

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
				"Anything is possible",
				"The only way to play the game is not to play at all",
			};

			Title = "Chat Sample";

			var otherSpeechBubbleStyle = new Style (typeof(VectorSpeechBubble)) {
				Setters = {
					// new Setter { Property = VectorSpeechBubble.ArrowDirectionProperty, Value = ArrowDirections.LeftBottom },
					// new Setter { Property = VectorSpeechBubble.HasShadowProperty, Value = true }, // Looks better without
					new Setter { Property = VectorSpeechBubble.FillColorProperty, Value = ColorHelper.MyLightBlue.ToFormsColor() },
					new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Start },
				}};
					
			var selfSpeechBubbleStyle = new Style (typeof(VectorSpeechBubble)) { 
				Setters = {
					// This is not working in a style (not sure why!)  Manually setting it in the controls for now
					// new Setter { Property = VectorSpeechBubble.ArrowDirectionProperty, Value = ArrowDirections.RightBottom },
					// new Setter { Property = VectorSpeechBubble.HasShadowProperty, Value = true }, // Looks better without
					new Setter { Property = VectorSpeechBubble.FillColorProperty, Value = ColorHelper.MyLightGray.ToFormsColor() },
					new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.End },
				}};

			this.Resources = new ResourceDictionary ();
			this.Resources.Add ("OtherSpeechBubbleStyle", otherSpeechBubbleStyle);
			this.Resources.Add ("SelfSpeechBubbleStyle", selfSpeechBubbleStyle);

			// Our chat bubbles will get added to this stack layout
			var bubbleStackLayout = new StackLayout { 
				Padding = new Thickness(4d,4d,4d,4d),
				Children = {
					new VectorSpeechBubble {
						Text = "Hello my friend! Its so good to chat to you :)",
						Style = otherSpeechBubbleStyle,
						ArrowDirection = ArrowDirections.LeftBottom,
					},
					new VectorSpeechBubble {
						Text = "Hey. How are ya?",
						Style = selfSpeechBubbleStyle,						
						ArrowDirection = ArrowDirections.RightBottom,
					},
					(_lastBubble = new VectorSpeechBubble {
						Text = "Couldn't be better, thanks for asking",
						Style = otherSpeechBubbleStyle,
						ArrowDirection = ArrowDirections.LeftBottom,
					}),
				},
			};
				
			var textEntryPanel = new TextEntryPanel { EnterButtonText = "Say it!", Placeholder = "Something to say?"};

			var mainLayout = new RelativeLayout ();
			// Scroll view fills up the entire space (including under the text input panel)
			// var innerScrollView = new ScrollView { Content = bubbleStackLayout };
			mainLayout.Children.Add (bubbleStackLayout,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent (parent => {
					return parent.Width;
				}),
				Constraint.RelativeToParent (parent => {
					return parent.Height - textEntryPanel.Height;
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

			// A second scroll view is required here and appears to work, despite recommendations not to use two scroll views (of the same direction together)
			// Without the second scroll view, the keyboard will rise up and cover the text input box.
			// Adding the second scroll view causes the text input to remain visible when the keyboard is displayed

			var outerScrollView = new ScrollView {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Content = mainLayout,
			};

			this.Content = outerScrollView;

			textEntryPanel.Focused += (sender, e) => {
				System.Diagnostics.Debug.WriteLine("textEntryPanel.Focused event fired");
				/*
				 * Unfortunately this fires when the keyboard has only just started to appear, so scrolling something into view now,
				 * causes it to be covered by the keyboard
				if (_lastBubble != null) {
					innerScrollView.ScrollToAsync(_lastBubble, ScrollToPosition.MakeVisible, animated:true);
				}*/

				// TODO: Scroll theh main scrollview (with the bubbles) into view
				// Somehow lock the secondary scroll view so that the text Entry field can't be scrolled off screen
			};

			textEntryPanel.TextEntered += (sender, e) => {
				if (!string.IsNullOrEmpty(e.Text)) {
					bubbleStackLayout.Children.Add(_lastBubble = new VectorSpeechBubble{ 
						Text = e.Text,
						Style = selfSpeechBubbleStyle,
						ArrowDirection = ArrowDirections.RightBottom });

					// After random time, the other person starts typing
					Device.StartTimer(TimeSpan.FromSeconds(_random.NextDouble()*1d), () => {
						var newBubble = new VectorSpeechBubble {
							Style = otherSpeechBubbleStyle,
							ArrowDirection = ArrowDirections.LeftBottom,
							IsTyping = true,
						};
						bubbleStackLayout.Children.Add(newBubble);
						// After a further random period, the text becomes visible as they finish typing
						Device.StartTimer(TimeSpan.FromSeconds(_random.NextDouble()*3d), () => {
							newBubble.Text = PickRandomResponse();
							newBubble.IsTyping = false;
							return false;
						});
						return false;
					});
				}
			};
		}

		private string PickRandomResponse() {
			if (!this.SampleResponses.Any ()) {
				return "I've nothing left to say";
			}
			var index = _random.Next (0, this.SampleResponses.Count - 1);
			var result = this.SampleResponses [index];
			this.SampleResponses.RemoveAt(index);

			return result;
		}
	}
}


