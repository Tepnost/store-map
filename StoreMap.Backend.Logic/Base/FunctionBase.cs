using System;

namespace StoreMap.Backend.Logic.Base
{
    public class FunctionBase
    {
        private readonly IServiceProvider serviceProvider;

        protected FunctionBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected TCommand ResolveCommand<TCommand>() where TCommand : ICommandBase
        {
            var command = serviceProvider.GetService(typeof(TCommand));

            if (command == null)
            {
                throw new ApplicationException($"{typeof(TCommand)} Command not found");
            }

            return (TCommand) command;
        } 
    }
}