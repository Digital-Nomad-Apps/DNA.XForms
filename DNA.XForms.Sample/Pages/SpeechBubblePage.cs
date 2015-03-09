using System;
using Xamarin.Forms;
using DNA.XForms;

namespace DNA.XForms.Sample
{
	public class SpeechBubblePage : ContentPage
	{
		SpeechBubbleView _firstBubble;
		SpeechBubbleView _replyBubble;

		public SpeechBubblePage ()
		{
			const string bubbleResource = "MessageBubble.png";

			// Setting styles in code is a bit clunky, but done here for demonstration purposes

			var selfBubbleStyle = new Style (typeof(SpeechBubbleView)) {
				Setters = { 
					new Setter { Property = SpeechBubbleView.ArrowDirectionProperty, Value = SpeechBubbleView.ArrowDirections.RightBottom },
					new Setter { Property = SpeechBubbleView.BubbleColorProperty, Value = ColorHelper.MyLightBlue },
					new Setter { Property = SpeechBubbleView.ImageResourceProperty, Value = bubbleResource },
					new Setter { Property = View.MinimumHeightRequestProperty, Value=30d },			
				}
			};

			var otherBubbleStyle = new Style (typeof(SpeechBubbleView)) {
				Setters = { 
					new Setter { Property = SpeechBubbleView.ArrowDirectionProperty, Value = SpeechBubbleView.ArrowDirections.LeftBottom },
					new Setter { Property = SpeechBubbleView.BubbleColorProperty, Value = ColorHelper.MyLightGray },
					new Setter { Property = SpeechBubbleView.ImageResourceProperty, Value = bubbleResource },
					new Setter { Property = SpeechBubbleView.IsTypingProperty, Value = true },	// Starts off typing
					new Setter { Property = View.MinimumHeightRequestProperty, Value=30d },
				}
			};

			_firstBubble = new SpeechBubbleView {
				Style = selfBubbleStyle,
				Text = "Hello there. Do you have a minute to talk?"
			};
			_replyBubble = new SpeechBubbleView { 
				IsTyping = true,
				Text = "That depends on what you want to say",
				Style = otherBubbleStyle,
			};


			this.Title = "Speech Bubble Sample";
			this.Content = new ScrollView {
				Content = new StackLayout {
					Children = {
						_firstBubble,
						_replyBubble,
					},
				}
			};

			// Testing:  force a re-render of the speech bubbles by setting the colors
			_firstBubble.BubbleColor = Color.Aqua;
			_replyBubble.BubbleColor = Color.Green;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (_replyBubble != null) {
				// Delay 3 seconds, then the other person finishes typing and some more text appears
				Device.StartTimer(TimeSpan.FromSeconds(3d), () => {
					_replyBubble.IsTyping = false;
					return false;
				});
			}
		}
	}
}

