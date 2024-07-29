using OlegDungeon.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static OlegDungeon.Wall;

namespace OlegDungeon
{
	public class Room
	{
		public Point Position;
		public Size Size;

		public void MakeHole(ref List<Cell> cells)
		{
			Random random = new Random(DateTime.Now.Millisecond);

			int y;
			int x;

			bool haveHole = false;

			while (!haveHole)
			{

				switch (random.Next(0, 5))
				{
					case 0:
						y = Position.Y - 1;

						MakeY(ref cells, -1);
						break;
					case 1:
						y = Position.Y + Size.Height;

						MakeY(ref cells, 1);
						break;
					case 2:
						x = Position.X;

						MakeX(ref cells, -1);
						break;
					case 4:
						x = Position.X + Size.Height;

						MakeX(ref cells, 1);
						break;
					default:
						break;
				}
			}

			void MakeY(ref List<Cell> cells1, int offset)
			{
				x = Position.X + random.Next(1, Size.Width - 1);

				Cell cell = cells1.FirstOrDefault(c => c.Position.Y == y && c.Position.X == x);
				Cell cellNext = cells1.FirstOrDefault(c => c.Position.Y == y + offset && c.Position.X == x);

				if (cell != null && cellNext != null && cellNext.State)
				{
					cell.IsRoom = true;
					haveHole = true;
				}
			}

			void MakeX(ref List<Cell> cells1, int offset)
			{
				y = Position.Y + random.Next(1, Size.Height - 1);

				Cell cell = cells1.FirstOrDefault(c => c.Position.Y == y && c.Position.X == x);
				Cell cellNext = cells1.FirstOrDefault(c => c.Position.Y == y && c.Position.X == x + offset);

				if (cell != null && cellNext != null && cellNext.State)
				{
					cell.IsRoom = true;
					haveHole = true;
				}
			}
		}
	}

	public static class PointExp
	{
		public static Point GetOffset(this Point point, int y, int x)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static Point Power(this Point point, int coef)
		{
			return new Point(point.X * coef, point.Y * coef);
		}

		public static Point Random(Random random, int maxY, int maxX)
		{
			return new Point(random.Next(0, maxX + 1), random.Next(0, maxY + 1));
		}
	}

	public class Wall
	{
		public Point Start { get; private set; }
		public Point End { get; private set; }

		public Sides Side { get; private set; }

		public Point Cell { get; private set; }

		public Wall(Point s, Point e, Sides side, Point cell)
		{
			this.Start = s;
			this.End = e;
			this.Side = side;
			this.Cell = cell;
		}

		public void Draw(Graphics graphics)
		{
			graphics.DrawLine(Pens.Black, Start, End);

		}

		public static Sides OppositSide(Sides side)
		{
			switch (side)
			{
				case Sides.Top:
					return Sides.Bottom;
				case Sides.Bottom:
					return Sides.Top;
				case Sides.Left:
					return Sides.Right;
				case Sides.Right:
					return Sides.Left;
			}

			return Sides.Error;
		}

		public static Sides RightSide(Sides side)
		{
			switch (side)
			{
				case Sides.Top:
					return Sides.Right;
				case Sides.Bottom:
					return Sides.Left;
				case Sides.Left:
					return Sides.Top;
				case Sides.Right:
					return Sides.Bottom;
			}

			return Sides.Error;
		}

		public static Sides LeftSide(Sides side)
		{
			switch (side)
			{
				case Sides.Top:
					return Sides.Left;
				case Sides.Bottom:
					return Sides.Right;
				case Sides.Left:
					return Sides.Bottom;
				case Sides.Right:
					return Sides.Top;
			}

			return Sides.Error;
		}

		public static Point DirectionFromSide(Sides side)
		{
			switch (side)
			{
				case Wall.Sides.Top:
					return new Point(0, -1);
				case Wall.Sides.Bottom:
					return new Point(0, 1);
				case Wall.Sides.Left:
					return new Point(-1, 0);
				case Wall.Sides.Right:
					return new Point(1, 0);
				default:
					throw new Exception("Пизда варёная");
			}
		}

		public Point[] Points 
		{
			get
			{
				return new Point[] { Start, End };
			}
		}

		public enum Sides 
		{
			Top = 0,
			Bottom = 1,
			Left = 2,
			Right = 3,
			Error = 4,
		}
	}
}
