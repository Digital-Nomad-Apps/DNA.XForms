using System;
using Xamarin.Forms;
using DNA.XForms;

namespace DNA.XForms.Sample
{
	public class VectorSpeechBubblePage : ContentPage
	{
		VectorSpeechBubble _firstBubble;
		VectorSpeechBubble _replyBubble;

		public VectorSpeechBubblePage ()
		{
			this.Title = "Vector Speech Bubble";
			this.BackgroundColor = ColorHelper.MyLightBlue.ToFormsColor();

			// Setting styles in code is a bit clunky, but done here for demonstration purposes
			// TODO: These don't seem to invoke a re-render on the custom renderer (are they even being set??)

			var selfBubbleStyle = new Style (typeof(VectorSpeechBubble)) {
				Setters = { 
					new Setter { Property = View.BackgroundColorProperty, Value = Color.Transparent },
					new Setter { Property = VectorSpeechBubble.ArrowDirectionProperty, Value = VectorSpeechBubble.ArrowDirections.DownRight },
					new Setter { Property = VectorSpeechBubble.ArrowDirectionProperty, Value = 1f }, // Bottom
					new Setter { Property = VectorSpeechBubble.FillColorProperty, Value = ColorHelper.MyLightBlue },
					new Setter { Property = View.MinimumHeightRequestProperty, Value=30d },			
				}
			};

			var otherBubbleStyle = new Style (typeof(VectorSpeechBubble)) {
				Setters = { 
					new Setter { Property = View.BackgroundColorProperty, Value = Color.Transparent },
					new Setter { Property = VectorSpeechBubble.ArrowDirectionProperty, Value = VectorSpeechBubble.ArrowDirections.DownLeft },
					new Setter { Property = VectorSpeechBubble.ArrowDirectionProperty, Value = 1f }, // Bottom
					new Setter { Property = VectorSpeechBubble.FillColorProperty, Value = ColorHelper.MyLightGray },
					new Setter { Property = VectorSpeechBubble.IsTypingProperty, Value = true },	// Starts off typing
					new Setter { Property = View.MinimumHeightRequestProperty, Value=30d },
				}
			};

			_firstBubble = new VectorSpeechBubble {
				Style = selfBubbleStyle,
				Text = "Hello there. Do you have a minute to talk?"
			};
			_replyBubble = new VectorSpeechBubble { 
				IsTyping = true,
				Text = "That depends on what you want to say",
				Style = otherBubbleStyle,
				MinimumHeightRequest = 54d,
				HeightRequest = 54d,
			};
				
			this.Content = new ScrollView {
				Content = new StackLayout {
					Children = {
						_firstBubble,
						_replyBubble,
					},
				}
			};

			// Testing:  force a re-render of the speech bubbles by setting the colors
			_firstBubble.FillColor = Color.Aqua;
			_replyBubble.FillColor = Color.Green;
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

