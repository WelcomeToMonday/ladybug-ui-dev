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
	private TextBox _textBox;

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

		_button.CursorEnter += delegate{Console.WriteLine("Button Entered!");};
		_button.CursorLeave += delegate{Console.WriteLine("Button Exited!"); _button.BackgroundImage = _buttonUpTexture;};

		_button.Focus += delegate{Console.WriteLine("Button Focused");};
		_button.UnFocus += delegate{Console.WriteLine("Button UnFocused");};

		_button.ClickStart += delegate{_button.BackgroundImage = _buttonDownTexture;};
		_button.ClickEnd += delegate{_button.BackgroundImage = _buttonUpTexture;};

		_button.Click += delegate{Console.WriteLine("click!");};

		_button.SetBounds(_button.Bounds.CopyAtOffset(30, 30));

		_textBox = new TextBox(_ui.RootPanel);

		_textBox.SetBounds(_textBox.Bounds.CopyAtPosition(120,120));

		_textBox.CursorEnter += delegate{Console.WriteLine("Textbox Entered");};
		_textBox.CursorLeave += delegate{Console.WriteLine("Textbox Exited");};

		_textBox.Focus += delegate{Console.WriteLine("Textbox Focused");};
		_textBox.UnFocus += delegate{Console.WriteLine("Textbox Unfocused");};
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
				_button.Bounds.CopyAtOffset(1,1)
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