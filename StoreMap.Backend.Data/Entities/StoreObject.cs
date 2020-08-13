using StoreMap.Backend.Data.Exceptions;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Entities
{
    public class StoreObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        public static StoreObject FromDto(StoreObjectDto dto)
        {
            if (dto is StoreObjectCircleDto circleDto)
            {
                return StoreObjectCircle.FromDto(circleDto);
            }

            if (dto is StoreObjectRectDto rectDto)
            {
                return StoreObjectRect.FromDto(rectDto);
            }
            
            throw new EntityConversionException(typeof(StoreObject), dto?.GetType());
        }
    }
}