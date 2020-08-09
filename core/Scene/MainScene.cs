using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.SceneManagement;
using Ladybug.Core.UI;

public class MainScene : Scene
{
	private UI _ui;
	private SpriteFont _font;
	public MainScene(SceneManager sceneManager) : base(sceneManager)
	{

	}

	public override void LoadContent()
	{
		_font = Content.Load<SpriteFont>("Fonts/font");
		base.LoadContent();
	}

	public override void Initialize()
	{
		CreateUI();
		spriteBatch = new SpriteBatch(SceneManager.GraphicsDevice);

		base.Initialize();
	}

	private void CreateUI()
	{
		var screenBounds = SceneManager.GraphicsDevice.PresentationParameters.Bounds;
		_ui = new UI(
			new Rectangle(0,0, screenBounds.Width, screenBounds.Height),
			_font
			);

		_ui.RootPanel.SetBounds(new Rectangle(0, 0, 200, 200));

		var button = new Button(_ui.RootPanel);

		//_ui.AddControl(button);
	}

	public override void Update(GameTime gameTime)
	{

	}

	public override void Draw(GameTime gameTime)
	{
		spriteBatch.Begin();
		_ui.Draw(spriteBatch);
		spriteBatch.End();
	}
}