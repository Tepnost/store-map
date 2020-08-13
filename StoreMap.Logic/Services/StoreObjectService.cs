using System.Collections.Generic;
using StoreMap.Data.Dtos;
using StoreMap.Logic.ServiceContracts;

namespace StoreMap.Logic.Services
{
    public class StoreObjectService : IStoreObjectService
    {
        public List<StoreObjectDto> GetStoreObjects()
        {
            return new List<StoreObjectDto>
            {
                new StoreObjectRectDto
                {
                    X = 0, Y = 0,
                    Width = 40, Height = 200
                },
                new StoreObjectRectDto
                {
                    X = 80, Y = 0,
                    Width = 40, Height = 200
                },
                new StoreObjectRectDto
                {
                    X = 160, Y = 0,
                    Width = 40, Height = 200
                },
                new StoreObjectCircleDto
                {
                    X = 300, Y = 100,
                    Diameter = 40
                }
            };
        }
    }
}