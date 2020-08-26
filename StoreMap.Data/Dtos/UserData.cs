using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoreMap.Data.Dtos
{
    public class UserData
    {
        public string Sub { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        [JsonPropertyName(Constants.RolesClaim)]
        public string Role { get; set; }
    }
}