using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Graphics;
using Ladybug.Graphics.BoxModel;

namespace Ladybug.Core.UI
{
	public class TextBox : Control
	{
		public TextBox(Control parentControl, string name ="") : base(parentControl, name)
		{
			PositionChanged += OnPositionChanged;
			Click += OnClick;
		}

		public Panel Panel { get; set; }
		public Label Label { get; set; }

		public override void Initialize()
		{
			Panel = new Panel(this);
			Label = new Label(this);

			Panel.BackgroundImage = null;
			Panel.SetBounds(
				new Rectangle(0, 0, 100, (int)Label.Font.MeasureString(" ").Y)
			);

			SetBounds(Panel.Bounds);
		}

		private void OnPositionChanged(object sender, EventArgs e)
		{
			Panel.SetBounds(Bounds, true);
			Label.SetBounds(Label.Bounds.CopyAtPosition
				(
					Panel.Bounds.GetHandlePosition(BoxHandle.BOTTOMLEFT),
					BoxHandle.BOTTOMLEFT
				),
				true
			);
		}

		private void OnClick(object sender, EventArgs e)
		{
			UI.SetActiveControl(this);
			UI.SceneManager.Window.TextInput += HandleTextInput;
		}

		public void HandleTextInput(object sender, TextInputEventArgs e)
		{

		}

		public override void Update()
		{
			if (UI.ActiveControl == this)
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
			Panel.Draw(spriteBatch);
		}
	}
}