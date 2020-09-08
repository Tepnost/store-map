using System;

namespace StoreMap.Data.Events
{
    public class ChangeFocusEvent
    {
        public Guid Id { get; set; }
        public bool IsFocused { get; set; }
    }
}