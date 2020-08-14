using Newtonsoft.Json;
using StoreMap.Data.Util;

namespace StoreMap.Data
{
    public static class Settings
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            SerializationBinder = new CustomSerializationBinder()
        };
    }
}