using System.Collections.Generic;
using System.Linq;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Entities
{
    public class Store : EntityBase
    {
        public string Name { get; set; }
        
        public List<StoreObject> StoreObjects { get; set; } = new List<StoreObject>();
        
        public List<StoreItem> StoreItems { get; set; } = new List<StoreItem>();

        public static Store FromDto(StoreDto dto)
        {
            return new Store
            {
                Id = dto.Id,
                Name = dto.Name,
                StoreObjects = dto.StoreObjects?.Select(StoreObject.FromDto).ToList() ?? new List<StoreObject>(),
                StoreItems = dto.StoreItems?.Select(StoreItem.FromDto).ToList() ?? new List<StoreItem>()
            };
        }

        public StoreDto ToDto()
        {
            return new StoreDto
            {
                Id = Id,
                Name = Name,
                StoreObjects = StoreObjects.Select(x => x.ToDto()).ToList(),
                StoreItems = StoreItems.Select(x => x.ToDto()).ToList()
            };
        }
    }
}