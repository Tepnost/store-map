using System;

namespace StoreMap.Data.Dtos
{
    public class StoreItemDto
    {
        public StoreItemDto() {}
        
        public StoreItemDto(StoreItemDto item)
        {
            X = item.X;
            Y = item.Y;
            Name = item.Name;
            Id = item.Id;
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}