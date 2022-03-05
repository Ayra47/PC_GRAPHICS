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

        Control draggedPiece = null;
        bool resizing = false;
        private Point startDraggingPoint;
        private Size startSize;
        Rectangle rectProposedSize = Rectangle.Empty;
        int resizingMargin = 5;

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draggedPiece = sender as Control;

            if ((e.X <= resizingMargin) || (e.X >= draggedPiece.Width - resizingMargin) ||
                (e.Y <= resizingMargin) || (e.Y >= draggedPiece.Height - resizingMargin))
            {
                resizing = true;

                this.Cursor = Cursors.SizeNWSE;

                this.startSize = new Size(e.X, e.Y);
                Point pt = this.PointToScreen(draggedPiece.Location);
                rectProposedSize = new Rectangle(pt, startSize);

                ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
            }
            else
            {
                resizing = false;
                this.Cursor = Cursors.SizeAll;
            }

            this.startDraggingPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPiece != null)
            {
                if (resizing)
                {
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                    rectProposedSize.Width = e.X - this.startDraggingPoint.X + this.startSize.Width;
                    rectProposedSize.Height = e.Y - this.startDraggingPoint.Y + this.startSize.Height;
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                else
                {
                    Point pt;
                    if (draggedPiece == sender)
                        pt = new Point(e.X, e.Y);
                    else
                        pt = draggedPiece.PointToClient((sender as Control).PointToScreen(new Point(e.X, e.Y)));

                    draggedPiece.Left += pt.X - this.startDraggingPoint.X;
                    draggedPiece.Top += pt.Y - this.startDraggingPoint.Y;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (resizing)
            {
                if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                {
                    ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                if (rectProposedSize.Width > 10 && rectProposedSize.Height > 10)
                {
                    this.draggedPiece.Size = rectProposedSize.Size;
                }
                else
                {
                    this.draggedPiece.Size = new Size((int)Math.Max(10, rectProposedSize.Width), Math.Max(10, rectProposedSize.Height));
                }
            }

            this.draggedPiece = null;
            this.startDraggingPoint = Point.Empty;
            this.Cursor = Cursors.Default;
        }
    }
    }

