using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreMap.Data.Attributes
{
    public class ReferenceCodeAttribute : Attribute
    {
        public List<string> ReferenceCodes { get; }

        public ReferenceCodeAttribute(string referenceCode)
        {
            ReferenceCodes = new List<string>{referenceCode};
        }

        public ReferenceCodeAttribute(string[] referenceCodes)
        {
            ReferenceCodes = referenceCodes.ToList();
        }
    }
}