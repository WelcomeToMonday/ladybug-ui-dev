using System;

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
}