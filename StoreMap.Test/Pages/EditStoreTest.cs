using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using StoreMap.Data.Dtos;
using StoreMap.Data.Responses;
using StoreMap.Logic.Interfaces;
using StoreMap.Pages;
using StoreMap.Test.Base;

namespace StoreMap.Test.Pages
{
    public class EditStoreTest : TestBase
    {
        private readonly Mock<IStoreService> storeServiceMock = new Mock<IStoreService>();
        private readonly Mock<IBrowserService> browserServiceMock = new Mock<IBrowserService>();

        private readonly StoreItemDto testStoreItem = new StoreItemDto
        {
            Id = Guid.NewGuid(),
            Name = "TestName",
            X = 1,
            Y = 2
        };
        
        [OneTimeSetUp]
        public void Setup()
        {
            Services.AddSingleton(browserServiceMock.Object);
            Services.AddSingleton(storeServiceMock.Object);
        }
        
        [Test]
        public void EditStore_ShouldSetStoreItemsOnInitialize()
        {
            MockGetStore();

            var component = RenderComponent<EditStore>();

            component.Instance.Store.Should().NotBeNull();
            component.Instance.StoreItems.Should().NotBeNullOrEmpty();
            var resultItem = component.Instance.StoreItems.First();
            resultItem.Id.Should().Be(testStoreItem.Id);
            resultItem.Name.Should().Be(testStoreItem.Name);
            resultItem.X.Should().Be(testStoreItem.X);
            resultItem.Y.Should().Be(testStoreItem.Y);
        }
        
        [Test]
        public void EditStore_WhenNoStoreItems_ShouldSetStoreItemsToEmptyList()
        {
            storeServiceMock
                .Setup(x => x.GetStore(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new StoreDto()));

            var component = RenderComponent<EditStore>();

            component.Instance.Store.Should().NotBeNull();
            component.Instance.StoreItems.Should().NotBeNull();
        }

        [Test]
        public void EditStore_Update_ShouldUpdateStoreItemsReference()
        {
            var newName = "new name for task";
            MockGetStore();

            var component = RenderComponent<EditStore>();

            component.Instance.StoreItems.First().Name = newName;
            component.Instance.Update();

            component.Instance.Store.StoreItems.Should().Contain(x => x.Name == newName);
        }

        [Test]
        public void EditStore_Save_ShouldSaveAndHandleResponse()
        {
            storeServiceMock.Invocations.Clear();
            
            var store = MockGetStore();
            var response = new GenericResponse<StoreDto>
            {
                Success = true,
                Message = "Test message",
                Data = store
            };
            
            storeServiceMock
                .Setup(x => x.SaveStore(It.IsAny<StoreDto>()))
                .Returns(Task.FromResult(response));

            var component = RenderComponent<EditStore>();
            
            component.Find(".save-store-btn").Click();
            
            storeServiceMock.Verify(x => x.SaveStore(It.Is<StoreDto>(y => y.Id == store.Id)), Times.Once);
        }

        [Test]
        public void EditStore_WhenNotFound_ShouldRedirect()
        {
            var navigatedTo = string.Empty;
            storeServiceMock
                .Setup(x => x.GetStore(It.IsAny<Guid>()))
                .Returns(Task.FromResult<StoreDto>(null));
            testNavigationManager.Navigated += (uri, load) => navigatedTo = uri;
            
            RenderComponent<EditStore>();

            navigatedTo.Should().NotBeNullOrEmpty();
        }
        
        private StoreDto MockGetStore()
        {
            var store = new StoreDto
            {
                Id = Guid.NewGuid(),
                StoreItems = new List<StoreItemDto>
                {
                    testStoreItem
                }
            };
            
            storeServiceMock
                .Setup(x => x.GetStore(It.IsAny<Guid>()))
                .Returns(Task.FromResult(store));

            return store;
        }
    }
}