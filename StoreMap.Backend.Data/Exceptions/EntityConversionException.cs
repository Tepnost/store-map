using System;

namespace StoreMap.Backend.Data.Exceptions
{
    public class EntityConversionException : Exception
    {
        public EntityConversionException(Type entityType, Type dtoType) : 
            base($"Can't convert type {dtoType} to entity type {entityType}")
        {
        }
    }
}