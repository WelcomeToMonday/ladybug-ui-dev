using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	public class UI
	{
		private Panel m_rootPanel;

		public UI(Rectangle bounds, SpriteFont defaultFont, Texture2D defaultBackground = null)
		{
			DefaultFont = defaultFont;
			RootPanel.SetBounds(bounds, true);
			RootPanel.Font = defaultFont;
			if (defaultBackground != null)
			{
				RootPanel.BackgroundImage = defaultBackground;
			}
		}
		
		/*
		public UI(string filePath)
		{
			// Load from file here
		}
		*/
		
		public Control this[string name] {get => RootPanel[name];}

		public SpriteFont DefaultFont {get; private set;}

		public Texture2D DefaultTexture {get; private set;}

		public Panel RootPanel
		{
			get
			{
				if (m_rootPanel == null)
				{
					m_rootPanel = new Panel();
					m_rootPanel.UI = this;
				}
				return m_rootPanel;
			}
		}

		public void AddControl(Control control)
		{
			RootPanel.AddControl(control);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			RootPanel.Draw(spriteBatch);
		}
	}
}