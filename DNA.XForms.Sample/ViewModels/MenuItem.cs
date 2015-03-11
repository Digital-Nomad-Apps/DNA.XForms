using Xamarin.Forms;
using System.Collections.Generic;

namespace DNA.XForms.Sample.ViewModels
{
	public  abstract class MenuItem
	{
		public abstract string Title { get; }
		// public virtual int Count { get; set; }
		public virtual bool Selected { get; set; }
		public virtual string Icon { get { return ""; } }
		public ImageSource IconSource { get { return ImageSource.FromFile(Icon); } }
	
		public static IEnumerable<MenuItem> All() {
			return new List<MenuItem> {
				new AboutMenuItem (),
				new CappedImageMenuItem (),
				new VectorSpeechBubbleMenuItem(),
				new RoundedFrameMenuItem(),
				new ChatSampleMenuItem(),
			};
		}
	}

	public class AboutMenuItem : MenuItem 
	{
		public override string Title { get { return "About"; }}
		public override string Icon { get { return "info_icon.png"; }}
	}

	public class VectorSpeechBubbleMenuItem : MenuItem 
	{
		public override string Title { get { return "Vector Speech Bubble"; }}
		public override string Icon { get { return "vector_bubble_icon.png"; }}
	}

	public class CappedImageMenuItem : MenuItem 
	{
		public override string Title { get { return "Capped Image"; }}
		public override string Icon { get { return "photo_icon.png"; }}
	}

	public class RoundedFrameMenuItem : MenuItem 
	{
		public override string Title { get { return "Rounded Frame"; }}
		public override string Icon { get { return "frame_icon.png"; }}
	}

	public class ChatSampleMenuItem : MenuItem
	{
		public override string Title { get { return "Chat Sample"; }}
		public override string Icon { get { return "speechbubble_icon.png"; }}
	}
}

