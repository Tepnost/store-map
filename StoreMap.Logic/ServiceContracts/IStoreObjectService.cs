using System.Collections.Generic;
using StoreMap.Data.Dtos;

namespace StoreMap.Logic.ServiceContracts
{
    public interface IStoreObjectService
    {
        List<StoreObjectDto> GetStoreObjects();
    }
}