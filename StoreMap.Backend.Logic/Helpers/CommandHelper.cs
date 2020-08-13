using System;
using System.Diagnostics;
using System.Net;
using StoreMap.Backend.Logic.Responses;

namespace StoreMap.Backend.Logic.Helpers
{
    public static class CommandHelper
    {
        [Conditional("DEBUG")]
        public static void AddErrorMessageIfDebug<T>(Exception e, GenericResponse<T> response)
        {
            response.Message = e.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}