using System;

using Microsoft.Xna.Framework;

namespace Ladybug.Core.UI
{
	public class UIControlChangeEvent : EventArgs
	{
		public UIControlChangeEvent(Control newControl, Control previousControl) : base()
		{
			NewControl = newControl;
			PreviousControl = previousControl;
		}

		public Control NewControl { get; private set; }
		public Control PreviousControl { get; private set; }
	}

	public class UIClickEvent : EventArgs
	{
		public UIClickEvent(Vector2 cursorPosition)
		{
			CursorPosition = cursorPosition;
		}

		public Vector2 CursorPosition { get; private set; }
	}

	public class ControlMoveEvent : EventArgs
	{
		public ControlMoveEvent(Vector2 oldPosition, Vector2 newPosition)
		{
			var delta = newPosition - oldPosition;
			XOffset = (int)delta.X;
			YOffset = (int)delta.Y;
		}

		public ControlMoveEvent(int xOffset, int yOffset)
		{
			XOffset = xOffset;
			YOffset = yOffset;
		}

		public ControlMoveEvent(Vector2 offset) : this((int)offset.X, (int)offset.Y)
		{

		}

		public int XOffset { get; private set; }

		public int YOffset { get; private set; }
	}
}