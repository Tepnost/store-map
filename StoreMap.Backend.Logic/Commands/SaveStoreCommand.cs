using System;
using System.Threading.Tasks;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Base;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Logic.Commands
{
    public class SaveStoreCommand : AsyncCommandBase<StoreDto, StoreDto>
    {
        private readonly IStoreRepository storeRepository;

        public SaveStoreCommand(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }
        
        protected override async Task<GenericResponse<StoreDto>> ExecuteCore(StoreDto request)
        {
            var store = Store.FromDto(request);
            if (request.Id == default)
            {
                store.Id = Guid.NewGuid();
                store = await storeRepository.Insert(store);
            }
            else
            {
                store = await storeRepository.Update(store);
            }

            return new GenericResponse<StoreDto>
            {
                Data = store.ToDto(),
                Success = true,
                Message = "Store saved successfully!"
            };
        }
    }
}