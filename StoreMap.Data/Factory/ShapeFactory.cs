using System;
using StoreMap.Data.Dtos;
using StoreMap.Data.Enums;

namespace StoreMap.Data.Factory
{
    public static class ShapeFactory
    {
        public static StoreObjectDto CreateShape(ShopObjectType type, int x, int y)
        {
            StoreObjectDto shape;
            switch (type)
            {
                case ShopObjectType.Rect:
                    shape = new StoreObjectRectDto();
                    break;
                case ShopObjectType.Circle:
                    shape = new StoreObjectCircleDto();
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