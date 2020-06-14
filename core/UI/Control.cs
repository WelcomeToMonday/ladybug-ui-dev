using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

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

		public event EventHandler Click;

		#endregion

		#region Properties

		public Rectangle Bounds { get; set; }

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

		#endregion
	}
}