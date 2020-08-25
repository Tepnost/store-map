using System;
using System.Collections.Generic;

namespace StoreMap.Data.Dtos
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public List<StoreObjectDto> StoreObjects { get; set; } = new List<StoreObjectDto>();

        public List<StoreItemDto> StoreItems { get; set; } = new List<StoreItemDto>();
    }
}