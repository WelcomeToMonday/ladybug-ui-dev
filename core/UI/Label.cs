using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Graphics;
using Ladybug.Graphics.BoxModel;

namespace Ladybug.Core.UI
{
	public class Label : Control
	{
		private TextSprite _textSprite;

		public Label(Control parentControl = null, string name = "") : base(parentControl, name)
		{
			PositionChanged += OnPositionChanged;
		}

		public override void Initialize()
		{
			/*
			_textSprite.SetBoundsToText();
			SetBounds(_textSprite.Bounds);
			*/
			SetText(Text);
		}

		public string Text {get; protected set;} = "Label";

		public void SetText (string newText)
		{
			Text = newText;
			if (_textSprite == null)
			{
				_textSprite = new TextSprite(newText, Font, new Rectangle(0, 0, 1000, 1000));
			}
			_textSprite.SetBounds(new Rectangle(
				_textSprite.Bounds.X, 
				_textSprite.Bounds.Y, 
				1000, 
				1000));
			_textSprite.SetText(newText);
			_textSprite.SetBoundsToText();
			SetBounds(_textSprite.Bounds, true);
		}

		private void OnPositionChanged(object sender, EventArgs e)
		{
			_textSprite.SetBoundsToText();
			_textSprite.SetBounds(
				_textSprite.Bounds.CopyAtPosition(Bounds.GetHandlePosition(BoxHandle.CENTER),BoxHandle.CENTER)
				);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			_textSprite?.Draw(spriteBatch);
		}
	}
}