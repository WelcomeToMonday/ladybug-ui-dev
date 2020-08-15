using System;
using System.Collections.Generic;
using System.Linq;

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

		#region Enums

		#endregion

		#region Events

		public event EventHandler Focus;
		public event EventHandler UnFocus;

		public event EventHandler PositionChanged;
		public event EventHandler SizeChanged;

		public event EventHandler Click;
		public event EventHandler ClickStart;
		public event EventHandler ClickHold;
		public event EventHandler ClickEnd;
		public event EventHandler ClickOut;

		public event EventHandler CursorEnter;
		public event EventHandler CursorLeave;

		#endregion

		#region Member Variables

		private bool _containsCursor = false;

		#endregion

		#region Constructors

		public Control()
		{

		}

		public Control(Control parentControl = null, string name = "") : this()
		{
			Name = name;
			if (parentControl != null)
			{
				parentControl.AddControl(this);
			}
			Initialize();
		}

		#endregion

		#region Properties

		public UI UI { get; set; }

		public Rectangle Bounds { get; set; }

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

		public bool HasFocus
		{
			get 
			{
				var res = false;
				if (UI != null)
				{
					res = UI.FocusedControl == this;
				}
				return res;
			}
		}

		public Texture2D BackgroundImage { get; set; }

		public SpriteFont Font { get; set; }

		#endregion

		#region Methods

		public virtual void Initialize()
		{
			if (UI != null)
			{
				UI.FocusChange += OnUIFocusChange;
				UI.ClickStart += OnUIClickStart;
				UI.ClickHold += OnUIClickHold;
				UI.ClickEnd += OnUIClickEnd;
			}
		}

		protected virtual void OnUIFocusChange(object sender, UIControlChangeEvent e)
		{
			if (e.NewControl == this)
			{
				Focus?.Invoke(this, new EventArgs());
			}

			if (e.PreviousControl == this)
			{
				UnFocus?.Invoke(this, new EventArgs());
			}
		}

		public virtual void OnUIClickStart(object sender, UIClickEvent e)
		{
			if (Bounds.Contains(e.CursorPosition))
			{
				ClickStart?.Invoke(this, new EventArgs());
			}
		}

		public virtual void OnUIClickHold(object sender, UIClickEvent e)
		{
			if (Bounds.Contains(e.CursorPosition))
			{
				ClickHold?.Invoke(this, new EventArgs());
			}
		}

		public virtual void OnUIClickEnd(object sender, UIClickEvent e)
		{
			if (Bounds.Contains(e.CursorPosition))
			{
				ClickEnd?.Invoke(this, new EventArgs());
				Click?.Invoke(this, new EventArgs());
			}
			else
			{
				ClickOut?.Invoke(this, new EventArgs());
			}
		}

		public virtual void Update()
		{
			if (Bounds.Contains(UI.CursorPosition))
			{
				if (!_containsCursor)
				{
					_containsCursor = true;
					CursorEnter?.Invoke(this, new EventArgs());
				}
			}
			else
			{
				if (_containsCursor)
				{
					_containsCursor = false;
					CursorLeave?.Invoke(this, new EventArgs());
				}
			}
			foreach (var c in Controls)
			{
				c.Update();
			}
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			foreach (var c in Controls)
			{
				c.Draw(spriteBatch);
			}
		}

		public void SetBounds(Rectangle newBounds, bool globalPositioning = false)
		{
			if (Parent == null)
			{
				globalPositioning = true;
			}

			Vector2 pos = globalPositioning ? newBounds.Location.ToVector2() : Parent.Bounds.Location.ToVector2() + newBounds.Location.ToVector2();
			Bounds = new Rectangle((int)pos.X, (int)pos.Y, newBounds.Width, newBounds.Height);
			PositionChanged?.Invoke(this, new EventArgs());
			SizeChanged?.Invoke(this, new EventArgs());
		}

		public void SetPosition(Vector2 newPos, bool globalPositioning = false)
		{
			Rectangle newBounds = new Rectangle((int)newPos.X, (int)newPos.Y, Bounds.Width, Bounds.Height);
			SetBounds(newBounds, globalPositioning);
		}

		public virtual void AddControl(Control newControl)
		{
			newControl.Parent = this;
			newControl.UI = UI;

			newControl.Font = UI?.DefaultFont;

			if (UI?.DefaultBackground != null) // keep an eye on this
			{
				newControl.BackgroundImage = UI.DefaultBackground;
			}

			Controls.Add(newControl);

			PositionChanged += newControl.OnParentPositionChange;
		}

		public Control FindControl(string name, bool recurse = false)
		{
			var res = Controls.Where(c => c.Name == name).FirstOrDefault();
			if (res == null && recurse)
			{
				foreach (var c in Controls)
				{
					res = c.FindControl(name, true);
					if (res != null) break;
				}
			}
			return res;
		}

		private void OnParentPositionChange(object sender, EventArgs e) => SetBounds(Bounds);

		#endregion
	}
}