using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Input;
using Ladybug.SceneManagement;

namespace Ladybug.Core.UI
{
	public class UI
	{

		public event EventHandler<UIControlChangeEvent> FocusChange;

		public event EventHandler<UIClickEvent> ClickStart;
		public event EventHandler<UIClickEvent> ClickHold;
		public event EventHandler<UIClickEvent> ClickEnd;

		private Panel m_rootPanel;

		private MouseMonitor _mouseMonitor;
		private KeyboardMonitor _keyboardMonitor;
		private GamepadMonitor _gamepadMonitor;

		public UI(UIConfig config)
		{
			DefaultFont = config.DefaultFont;
			RootPanel.SetBounds(config.Bounds, true);
			RootPanel.Font = config.DefaultFont;
			Inputs = config.Inputs;

			if (config.DefaultBackground != null)
			{
				DefaultBackground = config.DefaultBackground;
			}

			_mouseMonitor = new MouseMonitor();
			_keyboardMonitor = new KeyboardMonitor();
			_gamepadMonitor = new GamepadMonitor();

		}

		/*
		public UI(string filePath)
		{
			// Load from file here
		}
		*/

		public Control this[string name] { get => RootPanel[name]; }

		public SpriteFont DefaultFont { get; private set; }

		public Texture2D DefaultBackground { get; private set; }

		public Input Inputs { get; set; }

		public Control FocusedControl { get; private set; }

		public SceneManager SceneManager { get; set; }

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

		public Vector2 CursorPosition { get; private set; }

		public void AddControl(Control control)
		{
			RootPanel.AddControl(control);
		}

		public void SetFocus(Control control)
		{
			if (FocusedControl != control)
			{
				var oldControl = FocusedControl;

				FocusedControl = control;
				FocusChange?.Invoke(this, new UIControlChangeEvent(control, oldControl));
			}
			/*
			if (ActiveControl != null)
			{
				ActiveControl.DoDeactivate();
			}
			
			ActiveControl = control;
			control.DoActivate();
			*/
		}

		public void ClearFocus()
		{
			/*
			if (forceUnset)
			{
				ActiveControl = null;
			}
			else
			{
				if (ActiveControl == control)
				{
					ActiveControl = null;
				}
			}
			*/
			SetFocus(null);
		}

		private Vector2 GetCursorPosition()
		{
			Vector2 res = Vector2.Zero;

			if (Inputs.HasFlag(Input.Mouse))
			{
				res = _mouseMonitor.GetCursorPosition();
			}
			else
			{
				if (FocusedControl != null)
				{
					res = FocusedControl.Bounds.Center.ToVector2();
				}
			}

			return res;
		}

		private void HandleInput()
		{
			if (Inputs.HasFlag(Input.Mouse))
			{
				_mouseMonitor.BeginUpdate(Mouse.GetState());

				var cPos = _mouseMonitor.GetCursorPosition();

				if (_mouseMonitor.CheckButton(MouseButtons.LeftClick, InputState.Pressed)) ClickStart?.Invoke(this, new UIClickEvent(cPos)); //ActiveControl?.OnClickStart();
				if (_mouseMonitor.CheckButton(MouseButtons.LeftClick, InputState.Down)) ClickHold?.Invoke(this, new UIClickEvent(cPos));//ActiveControl?.OnClickHold();
				if (_mouseMonitor.CheckButton(MouseButtons.LeftClick, InputState.Released)) ClickEnd?.Invoke(this, new UIClickEvent(cPos)); //ActiveControl?.OnClickEnd();

				_mouseMonitor.EndUpdate();
			}
			if (Inputs.HasFlag(Input.Keyboard))
			{
				_keyboardMonitor.BeginUpdate(Keyboard.GetState());

				_keyboardMonitor.EndUpdate();
			}

			CursorPosition = GetCursorPosition();
		}

		public void Update()
		{
			HandleInput();
			RootPanel.Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			RootPanel.Draw(spriteBatch);
		}
	}
}