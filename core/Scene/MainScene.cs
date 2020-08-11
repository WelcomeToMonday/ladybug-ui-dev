using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Ladybug.SceneManagement;
using Ladybug.Graphics;
using Ladybug.Graphics.BoxModel;
using Ladybug.Core.UI;
using Ladybug.Input;

public class MainScene : Scene
{
	private UI _ui;
	private SpriteFont _font;
	private Texture2D _buttonUpTexture;
	private Texture2D _buttonDownTexture;

	private Button _button;

	private KeyboardMonitor _keyboardMonitor;

	public MainScene(SceneManager sceneManager) : base(sceneManager)
	{

	}

	public override void LoadContent()
	{
		_font = Content.Load<SpriteFont>("Fonts/font");
		_buttonUpTexture = Sprite.GetTextureFromColor(SceneManager.GraphicsDevice, Color.DarkSlateGray);
		_buttonDownTexture = Sprite.GetTextureFromColor(SceneManager.GraphicsDevice, Color.DarkSlateBlue);
		_keyboardMonitor = new KeyboardMonitor();
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

		var config = new UIConfig(SceneManager, _font)
		{
			Bounds = new Rectangle(0, 0, screenBounds.Width, screenBounds.Height),
			DefaultBackground = _buttonUpTexture
		};

		_ui = new UI(config);

		_ui.RootPanel.SetBounds(new Rectangle(0, 0, 200, 200));

		_button = new Button(_ui.RootPanel);

		_button.SetLabelText("X");

		_button.CursorEnter += delegate{Console.WriteLine("Cursor Entered!");};
		_button.CursorLeave += delegate{Console.WriteLine("Cursor Exited!"); _button.BackgroundImage = _buttonUpTexture;};

		_button.Activate += delegate{Console.WriteLine("Button Activated");};
		_button.Deactivate += delegate{Console.WriteLine("Button Deactivated");};

		_button.ClickStart += delegate{_button.BackgroundImage = _buttonDownTexture;};
		_button.ClickEnd += delegate{_button.BackgroundImage = _buttonUpTexture;};

		_button.Click += delegate{Console.WriteLine("click!");};

		_button.SetBounds(_button.Bounds.CopyAtOffset(30, 30));
	}

	public override void Update(GameTime gameTime)
	{
		_ui.Update();

		_keyboardMonitor.BeginUpdate(Keyboard.GetState());

		if (_keyboardMonitor.CheckButton(Keys.F, InputState.Released))
		{
			var debug = true; // for breakpoint
		}
		if (_keyboardMonitor.CheckButton(Keys.Right, InputState.Down))
		{
			_button.SetBounds(
				_button.Bounds.CopyAtOffset(1,1),
				true
			);
		}

		_keyboardMonitor.EndUpdate();
	}

	public override void Draw(GameTime gameTime)
	{
		spriteBatch.Begin();
		_ui.Draw(spriteBatch);
		spriteBatch.End();
	}
}