using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	[Flags]
	public enum Input
	{
		Mouse = 0,
		Keyboard = 1,
		Controller = 2,
	}
	public class UIConfig
	{
		public UIConfig(SpriteFont defaultFont)
		{
			DefaultFont = defaultFont;
		}

		public Input Inputs {get; set;} = Input.Mouse | Input.Keyboard;

		public SpriteFont DefaultFont {get; set;}

		public Texture2D DefaultBackground {get; set;}

		public Rectangle Bounds {get; set;} = new Rectangle(0, 0, 0, 0);
	}

}