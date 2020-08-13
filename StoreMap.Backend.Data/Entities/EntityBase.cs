using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace StoreMap.Backend.Data.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            CreatedOn = DateTime.Now;
        }
        
        [BsonId(IdGenerator =  typeof(GuidGenerator))]
        public Guid Id { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}