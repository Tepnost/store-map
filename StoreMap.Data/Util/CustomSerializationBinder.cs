using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace StoreMap.Data.Util
{
    public class CustomSerializationBinder : DefaultSerializationBinder
    {
        // List<> type is in different assemblies for .Net Standard and .Net Core
        // This solves type mapping problem
        private static readonly Regex Regex = new Regex(@"System\.Private\.CoreLib(, Version=[\d\.]+)?(, Culture=[\w-]+)(, PublicKeyToken=[\w\d]+)?");

        private static readonly ConcurrentDictionary<Type,(string assembly, string type)> Cache = new ConcurrentDictionary<Type, (string, string)>();

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            base.BindToName(serializedType, out assemblyName, out typeName);

            if (Cache.TryGetValue(serializedType, out var name))
            {
                assemblyName = name.assembly;
                typeName = name.type;
            }
            else
            {
                if (assemblyName.AsSpan().Contains("System.Private.CoreLib".AsSpan(), StringComparison.OrdinalIgnoreCase))
                    assemblyName = Regex.Replace(assemblyName, "mscorlib");

                if (typeName.AsSpan().Contains("System.Private.CoreLib".AsSpan(), StringComparison.OrdinalIgnoreCase))
                    typeName = Regex.Replace(typeName, "mscorlib");

                Cache.TryAdd(serializedType, (assemblyName, typeName));
            }
        }
    }
}