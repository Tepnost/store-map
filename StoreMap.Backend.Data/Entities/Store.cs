using System.Collections.Generic;
using System.Linq;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Entities
{
    public class Store : EntityBase
    {
        public string Name { get; set; }
        
        public List<StoreObject> StoreObjects { get; set; }

        public static Store FromDto(StoreDto dto)
        {
            return new Store
            {
                Name = dto.Name,
                StoreObjects = dto.StoreObjects.Select(StoreObject.FromDto).ToList()
            };
        }

        public StoreDto ToDto()
        {
            return new StoreDto
            {
                Id = Id,
                Name = Name,
                StoreObjects = StoreObjects.Select(x => x.ToDto()).ToList()
            };
        }
    }
}