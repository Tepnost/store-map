using System;

namespace StoreMap.Data.Dtos
{
    public class StoreObjectCircle : StoreObject
    {
        public int Diameter { get; set; }
        private int Radius => Diameter / 2;
        private int centerX => X + Radius;
        private int centerY => Y + Radius;

        public override string GetShapeStyles => $"width: {Diameter}px; height: {Diameter}px; border-radius: {Radius}px;";
        
        public override void UpdateSize(int currentX, int currentY, RectPosition boundaries = null)
        {
            var width = GetDistanceTo(X, Y, currentX, currentY);

            if (boundaries != null && 
               (X + width + boundaries.Left > boundaries.Right ||
                Y + width + boundaries.Top > boundaries.Bottom))
            {
                return;
            }
            
            Diameter = width;
        }

        private static int GetDistanceTo(int x1, int y1, int x2, int y2)
        {
            return (int) Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public override bool IsInside(int targetX, int targetY)
        {
            return GetDistanceTo(centerX, centerY, targetX, targetY) <= Radius;
        }
    }
}