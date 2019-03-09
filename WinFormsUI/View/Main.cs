using LifeEngine.ViewModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsUI.View
{
    public partial class Main : Form
    {
        private LifeSession _session;
        private Graphics _graphics;
        private const int scale = 10;

        public Main()
        {
            InitializeComponent();
            Shown += Main_Shown;
            MouseDown += Mouse_Click;
            _graphics = CreateGraphics();

            Random rnd = new Random(56464);
            bool[,] field = new bool[Width / scale, Height / scale];
            int i, j;

            for (i = 0; i < Width / scale; i++)
            {
                for (j = 0; j < Height / scale; j++)
                {
                    field[i, j] = false; // Convert.ToBoolean(rnd.Next(0, 2));
                }
            }

            _session = new LifeSession(field);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_Shown(object sender, EventArgs e)
        {
            _graphics.Clear(Color.White);
            //DrawGrid();
            DrawField();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _session.Update();
            DrawField();
        }

        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            _session.CurrentField[e.X / scale, e.Y / scale].Mode = LifeEngine.Model.CellMode.Alive;
            DrawField();
        }

        private void DrawField()
        {
            int i, j;

            for (i = 0; i < _session.CurrentField.Width; i++)
            {
                for (j = 0; j < _session.CurrentField.Height; j++)
                {
                    if (_session.CurrentField[i, j].Mode == LifeEngine.Model.CellMode.Alive)
                        _graphics.FillEllipse(new SolidBrush(Color.Black), scale * i, scale * j, scale, scale);
                    else
                        _graphics.FillEllipse(new SolidBrush(Color.White), scale * i, scale * j, scale, scale);
                }
            }
        }

        private void DrawGrid()
        {
            int i, j;

            for (i = 0; i < _session.CurrentField.Width; i++)
            {
                _graphics.DrawLine(new Pen(Color.Black), i * scale, 0, i * scale, Height);
            }
            
            for (j = 0; j < _session.CurrentField.Height; j++)
            {
                _graphics.DrawLine(new Pen(Color.Black), 0, j * scale, Width, j * scale);
            }
        }
    }
}
