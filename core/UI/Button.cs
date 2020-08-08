using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	public class Button : Control
	{
		private Label _label;
		private Panel _panel;
		public override void Draw(SpriteBatch spriteBatch)
		{
			_panel.Draw(spriteBatch);
			_label.Draw(spriteBatch);
		}
	}
}