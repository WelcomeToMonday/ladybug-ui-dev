using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Graphics;
using Ladybug.Graphics.BoxModel;

namespace Ladybug.Core.UI
{
	public class TextBox : Control
	{
		public TextBox(Control parentControl = null, string name ="") : base(parentControl, name)
		{
			PositionChanged += OnPositionChanged;
			ClickStart += OnClick;
			Focus += OnFocus;
			UnFocus += OnUnFocus;
		}

		public Panel Panel { get; set; }
		public Label Label { get; set; }

		public override void Initialize()
		{
			base.Initialize();	
			Panel = new Panel(this);
			Label = new Label(this);

			Panel.BackgroundImage = null;
			Panel.SetBounds(
				new Rectangle(0, 0, 400, (int)Label.Font.MeasureString(" ").Y + 10)
			);

			SetBounds(Panel.Bounds);
		}

		private void OnPositionChanged(object sender, EventArgs e)
		{
			Panel.SetBounds(Bounds, true);
			Label.SetBounds(Label.Bounds.CopyAtPosition
				(
					new Vector2(
						(int)Panel.Bounds.GetHandlePosition(BoxHandle.BOTTOMLEFT).X + 5,
						(int)Panel.Bounds.GetHandlePosition(BoxHandle.BOTTOMLEFT).Y - 5
					),
					BoxHandle.BOTTOMLEFT
				),
				true
			);
		}

		private void OnClick(object sender, EventArgs e)
		{
			UI.SetFocus(this);
		}

		public virtual void OnFocus(object sender, EventArgs e)
		{
			UI.SceneManager.Window.TextInput += HandleTextInput;
		}

		public virtual void OnUnFocus(object sender, EventArgs e)
		{
			UI.SceneManager.Window.TextInput -= HandleTextInput;
		}

		public void HandleTextInput(object sender, TextInputEventArgs e)
		{

		}

		public override void Update()
		{
			if (HasFocus)
			{
				//capture keyboard input
			}
			else
			{

			}

			base.Update();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (BackgroundImage != null)
			{
				spriteBatch.Draw
				(
					BackgroundImage,
					Bounds,
					null,
					Color.White
				);
			}
			Panel.Draw(spriteBatch);
			Label.Draw(spriteBatch);
		}
	}
}