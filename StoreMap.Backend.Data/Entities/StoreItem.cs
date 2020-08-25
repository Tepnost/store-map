using System;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Entities
{
    public class StoreItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public static StoreItem FromDto(StoreItemDto dto)
        {
            return new StoreItem
            {
                Id = dto.Id,
                Name = dto.Name,
                X = dto.X,
                Y = dto.Y
            };
        }

        public StoreItemDto ToDto()
        {
            return new StoreItemDto
            {
                Id = Id,
                Name = Name,
                X = X,
                Y = Y
            };
        }
    }
}