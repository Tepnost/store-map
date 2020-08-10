using System.Collections.Generic;
using StoreMap.Data.Dtos;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class StoreObjectService : IStoreObjectService
    {
        public List<StoreObject> GetStoreObjects()
        {
            return new List<StoreObject>
            {
                new StoreObjectSquare
                {
                    X = 0, Y = 0,
                    Width = 40, Height = 200
                },
                new StoreObjectSquare
                {
                    X = 80, Y = 0,
                    Width = 40, Height = 200
                },
                new StoreObjectSquare
                {
                    X = 160, Y = 0,
                    Width = 40, Height = 200
                },
                new StoreObjectCircle
                {
                    X = 300, Y = 100,
                    Radius = 40
                }
            };
        }
    }
}