using System.Net;

namespace StoreMap.Data.Responses
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static GenericResponse<T> AsSuccess(T data = default)
        {
            return new GenericResponse<T>()
            {
                Success = true, 
                Data = data,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static GenericResponse<T> AsFailure(string message = null, T data = default, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new GenericResponse<T>()
            {
                Success = false, 
                Data = data,
                Message = message ?? "Oops! Something went wrong...",
                StatusCode = status
            };
        }
    }
}