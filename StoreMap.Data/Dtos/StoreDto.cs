using System;
using System.Collections.Generic;

namespace StoreMap.Data.Dtos
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public List<StoreObjectDto> StoreObjects { get; set; } = new List<StoreObjectDto>();

        public List<StoreItemExtendedDto> StoreItems { get; set; } = new List<StoreItemExtendedDto>
        {
            new StoreItemExtendedDto
            {
                Id = Guid.NewGuid(),
                Name = "Test item",
                X = 50,
                Y = 50,
                IsFocused = true
            },
            new StoreItemExtendedDto
            {
                Id = Guid.NewGuid(),
                Name = "Test item 2",
                X = 100,
                Y = 100
            }
        };
    }
}