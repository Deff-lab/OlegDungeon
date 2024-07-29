using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlegDungeon
{
	public class Cell
	{
		public Point Position { get; private set; }

		private Settings settings;

		public bool State { get; set; }
		public bool IsRoom { get; set; }

		public Cell(Settings settings, Point position)
		{
			this.settings = settings;

			this.Position = position;
		}

		public Point ImagePoint
		{
			get
			{
				return new Point(Position.X * settings.RoomSize, Position.Y * settings.RoomSize);
			}
		}

		public void Draw(Graphics g)
		{
			Point p = ImagePoint;

			if (IsRoom)
			{
				g.DrawImage(settings.Room, p.X, p.Y, settings.RoomSize, settings.RoomSize);
				return;
			}

			if (State)
			{
				g.DrawImage(settings.Empty, p.X, p.Y, settings.RoomSize, settings.RoomSize);
				return;
			}

			g.DrawImage(settings.Black, p.X, p.Y, settings.RoomSize, settings.RoomSize);
		}
	}
}
