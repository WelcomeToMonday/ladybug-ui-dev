using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ladybug.Core.UI
{
	/// <summary>
	/// Base Class for all Menu Controls
	/// </summary>
	public abstract class Control
	{
		#region Constants

		private const int CHILD_CONTROL_COUNT_DEFAULT = 10;

		#endregion

		#region Events

		public event EventHandler GotFocus;
		public event EventHandler LostFocus;

		public event EventHandler PositionChanged;

		public event EventHandler Click;

		#endregion

		#region Properties

		public Rectangle Bounds { get; private set; }

		public Vector2 LocalPosition { get => Bounds.Location.ToVector2() - Parent.Bounds.Location.ToVector2(); }

		public Vector2 GlobalPosition { get => Bounds.Location.ToVector2(); }

		public Control Parent { get; set; }

		public string Name { get; set; }

		public int ID { get; set; }

		public List<Control> Controls
		{
			get
			{
				if (_controls == null)
				{
					_controls = new List<Control>(CHILD_CONTROL_COUNT_DEFAULT);
				}
				return _controls;
			}
			set => _controls = value;
		}
		private List<Control> _controls;

		public Dictionary<string, string> Attributes
		{
			get
			{
				if (_attributes == null)
				{
					_attributes = new Dictionary<string, string>();
				}
				return _attributes;
			}
			set => _attributes = value;
		}
		private Dictionary<string, string> _attributes;

		public bool Enabled { get; set; } = true;

		public bool Focused { get; set; }

		public Texture2D BackgroundImage { get; set; }

		public SpriteFont Font { get; set; }

		#endregion

		#region Methods

		public abstract void Draw(SpriteBatch spriteBatch);

		public void SetBounds(Rectangle newBounds, bool globalPositioning = false)
		{
			Vector2 pos = globalPositioning? newBounds.Location.ToVector2() : Parent.Bounds.Location.ToVector2() + newBounds.Location.ToVector2();
			Bounds = new Rectangle((int)pos.X, (int)pos.Y, newBounds.Width, newBounds.Height);
			PositionChanged?.Invoke(this, new EventArgs());
		}

		public void SetPosition(Vector2 newPos, bool globalPositioning = false)
		{
			Rectangle newBounds = new Rectangle((int)newPos.X, (int)newPos.Y, Bounds.Width, Bounds.Height);
			SetBounds(newBounds, globalPositioning);
		}

		public virtual void AddControl(Control newControl)
		{
			Controls.Add(newControl);
			PositionChanged += newControl.OnParentPositionChange;
		}

		private void OnParentPositionChange(object sender, EventArgs e) => SetBounds(Bounds);

		#endregion
	}
}