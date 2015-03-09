using Xamarin.Forms;

namespace DNA.XForms.Sample.ViewModels
{
	public  abstract class MenuItem
	{
		public abstract string Title { get; }
		// public virtual int Count { get; set; }
		public virtual bool Selected { get; set; }
		public virtual string Icon { get { return ""; } }
		public ImageSource IconSource { get { return ImageSource.FromFile(Icon); } }
	}

	public class AboutMenuItem : MenuItem 
	{
		public override string Title { get { return "About"; }}
		public override string Icon { get { return "info_icon.png"; }}
	}

	public class SpeechBubbleMenuItem : MenuItem 
	{
		public override string Title { get { return "Speech Bubble"; }}
		public override string Icon { get { return "speechbubble_icon.png"; }}
	}

	public class CappedImageMenuItem : MenuItem 
	{
		public override string Title { get { return "Capped Image"; }}
		public override string Icon { get { return "photo_icon.png"; }}
	}

	public class RoundedFrameMenuItem : MenuItem 
	{
		public override string Title { get { return "Rounded Frame"; }}
		public override string Icon { get { return ""; }}
	}
}

