using AntDesign;
using BlazorStyled;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using NUnit.Framework;
using StoreMap.Logic.ServiceContracts;
using TestContext = Bunit.TestContext;

namespace StoreMap.Test.Base
{
    public class TestBase : TestContext
    {
        protected readonly Mock<IMessageService> messageMock = new Mock<IMessageService>();
        protected readonly Mock<IJSRuntime> jsRuntimeMock = new Mock<IJSRuntime>();
        protected readonly TestNavigationManager testNavigationManager = new TestNavigationManager();
        
        [OneTimeSetUp]
        public void SetupBase()
        {
            Services.AddScoped(sp => messageMock.Object);
            Services.AddScoped(sp => jsRuntimeMock.Object);
            Services.AddSingleton<NavigationManager>(sp => testNavigationManager);
            Services.AddAntDesign();
            Services.AddBlazorStyled();
        }
    }
}