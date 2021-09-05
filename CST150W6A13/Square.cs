using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST150W6A13
{
    public class Square : Control, IGameObject
    {
        public new const int Size = 266;

        private Graphics g;
        private Marker marker;
        private bool _clicked;
        
        protected int X;
        protected int Y;
        protected new int Width;
        protected new int Height;

        public bool Clicked => _clicked;

        public Marker Marker => marker;
        

        public Square(Graphics context)
        {
            g = context;
            Click += Square_Click;
        }

        private void Square_Click(object sender, EventArgs e)
        {
            _clicked = true;
        }

        public new void Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveRelative(int xSpeed, int ySpeed)
        {
            X += xSpeed;
            Y += ySpeed;
        }

        public void SetMarker(Marker marker)
        {
            this.marker = marker;
        }

        public void Draw(TimeSpan deltaTime)
        {
            g.FillRectangle(Brushes.DarkGray, new(X, Y, Size, Size));
        }

        public void Initialize()
        {
            Width = Size;
            Height = Size;
        }

        public void Update(TimeSpan deltaTime)
        {
            // Update logic here
        }

        public static implicit operator Rectangle(Square a)
        {
            return new Rectangle(a.X * Size, a.Y * Size, a.Width, a.Height);
        }
    }
}
