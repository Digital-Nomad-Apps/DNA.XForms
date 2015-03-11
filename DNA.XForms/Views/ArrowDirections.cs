using System;

namespace DNA.XForms
{
	public enum ArrowDirections {
		None = 0,

		UpLeft = 1,
		UpCenter = 2,
		UpRight = 3,

		DownLeft = 4,
		DownCenter = 5,
		DownRight = 6,

		LeftTop = 7,
		LeftCenter = 8,
		LeftBottom = 9,

		RightTop = 10,
		RightCenter = 11,
		RightBottom = 12,
	}

	public static class ArrowDirectionsExtensions {
		public static bool IsUp(this ArrowDirections direction) {
			return (direction == ArrowDirections.UpLeft ||
				direction == ArrowDirections.UpCenter ||
				direction == ArrowDirections.UpRight);
		}
		public static bool IsDown(this ArrowDirections direction) {
			return (direction == ArrowDirections.DownLeft ||
				direction == ArrowDirections.DownCenter ||
				direction == ArrowDirections.DownRight);
		}
		public static bool IsLeft(this ArrowDirections direction) {
			return (direction == ArrowDirections.LeftTop ||
				direction == ArrowDirections.LeftCenter ||
				direction == ArrowDirections.LeftBottom);
		}
		public static bool IsRight(this ArrowDirections direction) {
			return (direction == ArrowDirections.RightTop ||
				direction == ArrowDirections.RightCenter ||
				direction == ArrowDirections.RightBottom);
		}
	}
}



