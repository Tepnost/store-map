using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoreMap.Data.Dtos
{
    public class UserData
    {
        public string Sub { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        [JsonPropertyName("https://store-map.com/assignedRoles")]
        public List<string> Roles { get; set; }
    }
}