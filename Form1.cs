using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПК_ГРАФИКА_ЛАБА1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = CreateGraphics();
            g.DrawEllipse(Pens.Black, 100, 100, 200, 200);
            g.DrawRectangle(Pens.Black, 300, 300, 200, 200);
            g.DrawLine(Pens.Black, 600, 600, 700, 800);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            g = CreateGraphics();
            switch (e.KeyCode)
            {
                case Keys.A:
                    g.DrawRectangle(Pens.Black, 600, 600, 200, 200);
                    break;
                case Keys.Delete:
                    g.Clear(Color.White);
                    break;
            }
        }
    }
}
