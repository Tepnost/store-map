using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StoreMap.Backend.Data.Entities
{
    public class Store
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}