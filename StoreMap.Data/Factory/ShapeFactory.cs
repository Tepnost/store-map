using System;
using StoreMap.Data.Dtos;
using StoreMap.Data.Enums;

namespace StoreMap.Data.Factory
{
    public static class ShapeFactory
    {
        public static StoreObject CreateShape(ShapeType type, int x, int y)
        {
            StoreObject shape;
            switch (type)
            {
                case ShapeType.Rect:
                    shape = new StoreObjectSquare();
                    break;
                case ShapeType.Circle:
                    shape = new StoreObjectCircle();
                    break;
                default:
                    throw new ArgumentException("Incorrect shape type");
            }

            shape.X = x;
            shape.Y = y;

            return shape;
        } 
    }
}