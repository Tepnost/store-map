using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StoreMap.Data.Attributes;

namespace StoreMap.Data.Extensions
{
    public static class EnumExtensions
    {
        public static List<string> GetRefCodesList(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<ReferenceCodeAttribute>()
                .ReferenceCodes;
        }
        
        public static string GetRefCodes(this Enum enumValue)
        {
            var codes = enumValue.GetType().GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<ReferenceCodeAttribute>()
                .ReferenceCodes;
        
            return string.Join(",", codes);
        }
    }
}