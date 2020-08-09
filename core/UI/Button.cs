using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	public class Button : Control
	{

		public Button(Control parentControl = null, string name = "") : base (parentControl, name)
		{

		}

		public override void Initialize()
		{
			//UI = parentControl?.UI;

			Panel = new Panel(this);
			AddControl(Panel);

			Label = new Label(this);
			AddControl(Label);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Panel.Draw(spriteBatch);
			Label.Draw(spriteBatch);
		}

		public Panel Panel {get; set;}
		public Label Label {get; set;}
	}
}