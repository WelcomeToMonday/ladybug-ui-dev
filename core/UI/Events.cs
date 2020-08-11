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
		
		public Vector2 CursorPosition {get; private set;}
	}
}