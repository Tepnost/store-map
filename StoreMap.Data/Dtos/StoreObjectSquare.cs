using System;

namespace StoreMap.Data.Dtos
{
    public class StoreObjectSquare : StoreObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private bool isInversedX;
        private bool isInversedY;
        private int MaxX => X + Width;
        private int MaxY => Y + Height;

        public override string GetShapeStyles => $"width: {Width}px; height: {Height}px;";
        
        public override void UpdateSize(int currentX, int currentY, RectPosition boundaries = null)
        {
            SetX(currentX);
            SetY(currentY);
        }

        private void SetX(int currentX)
        {
            var w = currentX - X;

            if (w < 0)
            {
                var absW = Math.Abs(w);
                X -= absW;
                Width += absW;
                isInversedX = true;
            }
            else
            {
                if (isInversedX && currentX < X + Width)
                {
                    Width -= currentX - X;
                    X = currentX;
                }
                else
                {
                    isInversedX = false;
                    Width = w;
                }
            }
        }

        private void SetY(int currentY)
        {
            var h = currentY - Y;

            if (h < 0)
            {
                isInversedY = true;
                var absH = Math.Abs(h);
                Y -= absH;
                Height += absH;
            }
            else
            {
                if (isInversedY && currentY < Y + Height)
                {
                    Height -= currentY - Y;
                    Y = currentY;
                }
                else
                {
                    isInversedY = false;
                    Height = h;
                }
            }
        }

        public override bool IsInside(int targetX, int targetY)
        {
            return targetX >= X && targetX <= MaxX &&
                   targetY >= Y && targetY <= MaxY;
        }
    }
}