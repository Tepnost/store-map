using Newtonsoft.Json;

namespace StoreMap.Data
{
    public static class Settings
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
    }
}