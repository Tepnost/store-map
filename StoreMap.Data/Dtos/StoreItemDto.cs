using System;

namespace StoreMap.Data.Dtos
{
    public class StoreItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}