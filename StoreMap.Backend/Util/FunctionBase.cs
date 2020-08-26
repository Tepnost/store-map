using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMap.Backend.Logic.Base;
using StoreMap.Backend.Logic.ServiceContracts;
using StoreMap.Data.Dtos;
using StoreMap.Data.Enums;
using StoreMap.Data.Extensions;
using StoreMap.Data.Responses;

namespace StoreMap.Backend.Util
{
    public class FunctionBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IUserService userService;

        public FunctionBase(IServiceProvider serviceProvider, IUserService userService)
        {
            this.serviceProvider = serviceProvider;
            this.userService = userService;
        }

        public TCommand ResolveCommand<TCommand>() where TCommand : ICommandBase
        {
            var command = serviceProvider.GetService(typeof(TCommand));

            if (command == null)
            {
                throw new ApplicationException($"{typeof(TCommand)} Command not found");
            }

            return (TCommand) command;
        }

        public async Task<IActionResult> SafeExecute<T>(Func<Task<GenericResponse<T>>> action)
        {
            try
            {
                var result = await action();
                return new StatusWithDataResult(result.StatusCode, result);
            }
            catch (HttpResponseException e)
            {
                return new StatusWithDataResult(e.Response.StatusCode, GenericResponse<object>.AsFailure());
            }
        }
        
        public async Task<UserData> ValidateTokenAsync(HttpRequest req, UserRole? userRole = UserRole.All)
        {
            var value = req.Headers["Authorization"].ToString();
            if (value?.StartsWith("Bearer ") != true)
                return null;

            var tokenString = value.Split(" ").Last();

            var user = await userService.GetUserData(tokenString);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var allowedRoles = userRole.GetRefCodesList();
            if (user.Role != null && !allowedRoles.Contains(user.Role))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            
            return user;
        }
    }
}