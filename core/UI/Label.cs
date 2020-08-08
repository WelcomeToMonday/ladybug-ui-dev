using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Graphics;

namespace Ladybug.Core.UI
{
	public class Label : Control
	{
		private TextSprite _textSprite;
		public override void Draw(SpriteBatch spriteBatch)
		{
			_textSprite.Draw(spriteBatch);
		}
	}
}