using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	public class TextBox : Control
	{
		private Panel _panel;
		private Label _label;

		public override void Draw(SpriteBatch spriteBatch)
		{
			_panel.Draw(spriteBatch);
			_label.Draw(spriteBatch);
		}
	}
}