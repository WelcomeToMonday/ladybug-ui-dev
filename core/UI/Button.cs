using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Ladybug.Graphics;
using Ladybug.Graphics.BoxModel;

namespace Ladybug.Core.UI
{
	public class Button : Control
	{

		public Button(Control parentControl = null, string name = "") : base (parentControl, name)
		{
			CursorEnter += OnCursorEnter;
			CursorLeave += OnCursorExit;
			PositionChanged += OnPositionChanged;
		}

		public override void Initialize()
		{
			//UI = parentControl?.UI;

			Panel = new Panel(this);
			Label = new Label(this);

			Panel.BackgroundImage = null;

			Panel.SetBounds(
				new Rectangle(
					(int)Label.Bounds.X,
					(int)Label.Bounds.Y,
					Label.Bounds.Width + 40,
					Label.Bounds.Height + 20
					)
			);

			SetBounds(Panel.Bounds);

			Label.SetBounds(
				Label.Bounds.CopyAtPosition(
					Panel.Bounds.GetHandlePosition(BoxHandle.CENTER), 
					BoxHandle.CENTER), 
				true
			);
		}

		private void OnCursorEnter(object sender, EventArgs e)
		{
			UI.SetActiveControl(this);
		}

		private void OnCursorExit(object sender, EventArgs e)
		{
			UI.UnsetActiveControl(this);
		}

		private void OnPositionChanged(object sender, EventArgs e)
		{
			Panel.SetBounds(Bounds, true);
			SetLabelText(Label.Text);
		}

		public void SetLabelText(string labelText)
		{
			Label.SetText(labelText);
			Label.SetBounds(
				Label.Bounds.CopyAtPosition(
					Panel.Bounds.GetHandlePosition(BoxHandle.CENTER), 
					BoxHandle.CENTER), 
				true
			);
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

		public Panel Panel {get; set;}
		public Label Label {get; set;}
	}
}