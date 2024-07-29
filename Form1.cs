using OlegDungeon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlegDungeon
{
	public partial class Form1 : Form
	{
		List<Cell> cells = new List<Cell>();

		Settings settings = new Settings();

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			currentCells.Clear();
			rooms.Clear();
			InitCells();

			Cell cell = cells.First(c => c.Position.X == 1 && c.Position.Y == 1);

			cell.State = true;

			currentCells.Add(cell);

			Draw();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			while (currentCells.Any())
				Step();

			foreach (Room room in rooms)
				room.MakeHole(ref cells);

			Draw();
		}

		void Step()
		{
			if (!currentCells.Any())
				return;

			Random random = new Random(DateTime.Now.Millisecond);

			Cell cell = currentCells[random.Next(0, currentCells.Count)];

			List<Cell> nextCells = FindNext(cell);

			if (!nextCells.Any())
			{
				currentCells.Remove(cell);
				return;
			}

			Cell next = nextCells[random.Next(0, nextCells.Count)];

			currentCells.Add(next);

			next.State = true;

			Cell between = null;

			if (cell.Position.Y > next.Position.Y)
				between = GetCellFromPosition(cell.Position.GetOffset(-1, 0));

			if (cell.Position.Y < next.Position.Y)
				between = GetCellFromPosition(cell.Position.GetOffset(1, 0));

			if (cell.Position.X > next.Position.X)
				between = GetCellFromPosition(cell.Position.GetOffset(0, -1));

			if (cell.Position.X < next.Position.X)
				between = GetCellFromPosition(cell.Position.GetOffset(0, 1));

			if (between != null)
				between.State = true;
		}

		List<Cell> FindNext(Cell cell)
		{
			List<Cell> nextCells = new List<Cell>();

			AddNextCell(cell.Position.GetOffset(0, -2), ref nextCells);
			AddNextCell(cell.Position.GetOffset(0, 2), ref nextCells);
			AddNextCell(cell.Position.GetOffset(2, 0), ref nextCells);
			AddNextCell(cell.Position.GetOffset(-2, 0), ref nextCells);

			return nextCells;
		}

		void AddNextCell(Point position, ref List<Cell> nextCells)
		{
			Cell nextCell = GetCellFromPosition(position);

			if (nextCell == null || nextCell.State)
				return;
			
			nextCells.Add(nextCell);
		}

		List<Cell> currentCells = new List<Cell>();

		Cell GetCellFromPosition(Point point)
		{
			return cells.FirstOrDefault(c => c.Position.X == point.X && c.Position.Y == point.Y);
		}

		public void InitCells()
		{
			cells.Clear();

			for (int i = 0; i < settings.CellByLine; i++)
			{
				for (int j = 0; j < settings.CellByLine; j++)
				{
					Cell cell = new Cell(settings, new Point(i, j));
					cells.Add(cell);
				}
			}
		}

		List<Room> rooms = new List<Room>();

		void MakeRoom(int y, int x, int height, int width)
		{
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Cell cell = GetCellFromPosition(new Point(y + i, x + j));

					if (cell != null)
					{
						cell.State = true;
						cell.IsRoom = true;
					}
				}
			}

			rooms.Add(new Room()
			{
				Position = new Point(y, x),
				Size = new Size(height, width),
			});
		}

		public void Draw()
		{
			Bitmap bitmap = new Bitmap(settings.Size, settings.Size);

			using (Graphics g = Graphics.FromImage(bitmap))
			{
				foreach (Cell cell in cells)
				{
					cell.Draw(g);
				}
			}

			pictureBox1.Image = bitmap;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Random random = new Random(DateTime.Now.Millisecond);

			int some = 4;

			int y = random.Next(0, settings.CellByLine / some) * some - 1;
			int x = random.Next(0, settings.CellByLine / some) * some - 1;

			int width = random.Next(2, 5) * 2 + 1;
			int height = random.Next(2, 5) * 2 + 1;

			MakeRoom(y, x, width, height);

			Draw();
		}
	}
}
