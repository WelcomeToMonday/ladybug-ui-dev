using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	public class Panel : Control
	{
		public Panel(Control parentControl = null, string name = "") : base(parentControl, name)
		{

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Visible)
			{
				if (BackgroundImage != null)
				{
					spriteBatch.Draw(
						BackgroundImage,
						Bounds,
						null,
						Color.White
					);
				}
				base.Draw(spriteBatch);
			}
		}
	}
}