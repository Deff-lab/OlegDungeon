using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlegDungeon
{
	public class Settings
	{
		public Bitmap Black { get; private set; }
		public Bitmap Empty { get; private set; }
		public Bitmap Room { get; private set; }

		public int RoomSize { get; private set; } = 40;

		public int Size { get; private set; } = 1240;

		public int CellByLine
		{
			get
			{
				return Size / RoomSize;
			}
		}

		public Settings()
		{
			Black = new Bitmap(10, 10);

			using (Graphics g = Graphics.FromImage(Black))
			{
				g.FillRectangle(Brushes.Black, new Rectangle(-1, -1, 11, 11));
			}

			Empty = new Bitmap(10, 10);

			using (Graphics g = Graphics.FromImage(Empty))
			{
				g.FillRectangle(Brushes.LightCyan, new Rectangle(-1, -1, 11, 11));
			}

			Room = new Bitmap(10, 10);

			using (Graphics g = Graphics.FromImage(Room))
			{
				g.FillRectangle(Brushes.LightSalmon, new Rectangle(-1, -1, 11, 11));
			}
		}
	}
}
