using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StoreMap.Data.Attributes;

namespace StoreMap.Data.Extensions
{
    public static class EnumExtensions
    {
        public static List<string> GetReferenceCodes(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<ReferenceCodeAttribute>()?
                .ReferenceCodes;
        }
    }
}