using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMap.Data;

namespace StoreMap.Backend
{
    public class StatusWithDataResult : ActionResult
    {
        private readonly HttpStatusCode statusCode;
        private readonly object data; // TODO: can I use generic type here?

        public StatusWithDataResult(HttpStatusCode statusCode, object data = null)
        {
            this.statusCode = statusCode;
            this.data = data;
        }

        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.StatusCode = (int) statusCode;
            response.ContentType = "application/json";
            
            using var writer = new StreamWriter(response.Body);
            using var jsonWriter = new JsonTextWriter(writer) {CloseOutput = false, AutoCompleteOnClose = false};
            var jsonSerializer = JsonSerializer.Create(Settings.JsonSerializerSettings);
            jsonSerializer.Serialize(jsonWriter, data);
        }
    }
}