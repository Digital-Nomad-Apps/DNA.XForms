using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace DNA.XForms.Sample
{
	public class ColorPicker : Picker
	{
		// TODO: Add data-binding capabilities to the ColorPicker
		// Perhaps from here: https://gist.github.com/Sankra/286cfbdd5dfd379a9155

		// TODO: Create custom rendering so you can see the colors in the list while picking
		// TODO: fix the Black text on Black background issue when Black color selected 

		// Dictionary to get Color from color name.
		Dictionary<string, Color> Colors = new Dictionary<string, Color>
		{
			{ "None (Default)", Color.Default },
			{ "Aqua", Color.Aqua }, 
			{ "Black", Color.Black },
			{ "Blue", Color.Blue }, 
			{ "Gray", Color.Gray }, 
			{ "Green", Color.Green },
			{ "Lime", Color.Lime }, 
			{ "Maroon", Color.Maroon },
			{ "Navy", Color.Navy }, 
			{ "Olive", Color.Olive },
			{ "Purple", Color.Purple }, 
			{ "Red", Color.Red },
			{ "Silver", Color.Silver }, 
			{ "Teal", Color.Teal },
			{ "Transparent", Color.Transparent },
			{ "White", Color.White }, 
			{ "Yellow", Color.Yellow },
			{ "Custom Blue", Color.FromRgb(89, 117, 158) },
		};

		#region BindableProperty Definitions

		public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create<ColorPicker, Color>(p => p.SelectedColor, Color.Default);

		public Color SelectedColor {
			get { return (Color)base.GetValue (SelectedColorProperty); }
			set { 

				this.SelectedIndex = this.Items.IndexOf (Colors.Where (c => c.Value == value).First ().Key);
				base.SetValue (SelectedColorProperty, value); 
			}
		}

		#endregion

		public event EventHandler SelectedColorChanged;

		/// <summary>
		/// Adds a Color to the collection
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="value">Value.</param>
		public void AddColor(string text, Color value) {
			this.Items.Add (text);
			Colors.Add(text, value);
		}

		/// <summary>
		/// Clear the Colors collection (if you want to use your own)
		/// </summary>
		public void ClearColors() {
			this.Items.Clear ();
			Colors.Clear ();
		}

		public void RemoveColor(string color) {
			this.Items.Remove (color);
			Colors.Remove (color);
		}

		public ColorPicker ()
		{
			Title = "Color";

			foreach (var item in this.Colors) {
				this.Items.Add (item.Key);
			}
			this.SelectedIndex = 0; // None (Default)

			this.SelectedIndexChanged += (sender, args) =>
			{
				if (this.SelectedIndex == -1)
				{
					this.SelectedColor = Color.Default;
					this.BackgroundColor = Color.Default;
				}
				else
				{
					string colorName = this.Items[this.SelectedIndex];
					var color = Colors[colorName];
					this.SelectedColor = color;
					this.BackgroundColor = color; 
				}

				if (this.SelectedColorChanged != null)
					this.SelectedColorChanged(this, new EventArgs());
			};
		}
	}
}


