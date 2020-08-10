using System;

namespace StoreMap.Data.Dtos
{
    public class StoreObjectCircle : StoreObject
    {
        public double Diameter { get; set; }

        public override string GetShapeStyles => $"width: {Diameter}px; height: {Diameter}px; border-radius: {Diameter/2}px;";
        
        public override void UpdateSize(int currentX, int currentY, RectPosition boundaries = null)
        {
            var width = Math.Sqrt(Math.Pow(X - currentX, 2) + Math.Pow(Y - currentY, 2));

            if (X + width + boundaries.Left > boundaries.Right ||
                Y + width + boundaries.Top > boundaries.Bottom)
            {
                return;
            }
            
            Diameter = width;
        }
    }
}