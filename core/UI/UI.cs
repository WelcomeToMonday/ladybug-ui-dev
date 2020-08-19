using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Input;
using Ladybug.ECS;
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

		protected MouseMonitor MouseMonitor { get; set; }
		protected KeyboardMonitor KeyboardMonitor { get; set; }
		protected GamepadMonitor GamepadMonitor { get; set; }

		public UI(UIConfig config)
		{
			DefaultFont = config.DefaultFont;
			RootPanel.SetBounds(config.Bounds);
			RootPanel.Font = config.DefaultFont;
			Inputs = config.Inputs;
			SceneManager = config.SceneManager;

			if (config.DefaultBackground != null)
			{
				DefaultBackground = config.DefaultBackground;
			}

			MouseMonitor = new MouseMonitor();
			KeyboardMonitor = new KeyboardMonitor();
			GamepadMonitor = new GamepadMonitor();

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

		public ResourceCatalog Catalog { get; set; }

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
				res = MouseMonitor.GetCursorPosition();
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

		protected virtual void HandleInput()
		{
			if (Inputs.HasFlag(Input.Mouse))
			{
				MouseMonitor.BeginUpdate(Mouse.GetState());

				var cPos = MouseMonitor.GetCursorPosition();

				if (MouseMonitor.CheckButton(MouseButtons.LeftClick, InputState.Pressed))
				{
					ClickStart?.Invoke(this, new UIClickEvent(cPos));
				}

				if (MouseMonitor.CheckButton(MouseButtons.LeftClick, InputState.Down))
				{
					ClickHold?.Invoke(this, new UIClickEvent(cPos));
				}

				if (MouseMonitor.CheckButton(MouseButtons.LeftClick, InputState.Released))
				{
					ClickEnd?.Invoke(this, new UIClickEvent(cPos));
				}

				MouseMonitor.EndUpdate();
			}
			if (Inputs.HasFlag(Input.Keyboard))
			{
				KeyboardMonitor.BeginUpdate(Keyboard.GetState());

				if (KeyboardMonitor.CheckButton(Keys.Escape, InputState.Down)) ClearFocus();

				KeyboardMonitor.EndUpdate();
			}

			CursorPosition = GetCursorPosition();
		}

		public virtual void Update()
		{
			HandleInput();
			RootPanel.Update();
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			RootPanel.Draw(spriteBatch);
		}
	}
}