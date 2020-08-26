using System;
using StoreMap.Data.Responses;

namespace StoreMap.Data.Exceptions
{
    public class FailedApiRequestException<T> : Exception
    {
        public GenericResponse<T> Response { get; }

        public FailedApiRequestException(GenericResponse<T> response) : base(response.Message)
        {
            Response = response;
        }
    }
}