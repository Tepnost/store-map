using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Entities
{
    public class StoreObjectRect : StoreObject
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public static StoreObjectRect FromDto(StoreObjectRectDto dto)
        {
            return new StoreObjectRect
            {
                X = dto.X,
                Y = dto.Y,
                Color = dto.Color,
                Height = dto.Height,
                Width = dto.Width
            };
        }
    }
}