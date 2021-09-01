using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST150W6A13
{
    public class Square : IGameObject
    {
        public const int Size = 266;

        private Graphics g;
        private Marker marker;
        
        protected int X;
        protected int Y;
        protected int Width;
        protected int Height;

        public Marker Marker => marker;
        

        public Square(Graphics context)
        {
            g = context;
        }

        public void Move(int x, int y)
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
    }
}
