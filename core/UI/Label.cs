using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Graphics;

namespace Ladybug.Core.UI
{
	public class Label : Control
	{
		private TextSprite _textSprite;

		public Label(Control parentControl = null, string name = "") : base(parentControl, name)
		{

		}

		public override void Initialize()
		{
			_textSprite = new TextSprite(Text, Font, new Rectangle(0, 0, 1000, 1000));
			_textSprite.SetBoundsToText();
		}

		public string Text {get; set;} = "Label";

		public override void Draw(SpriteBatch spriteBatch)
		{
			_textSprite?.Draw(spriteBatch);
		}
	}
}