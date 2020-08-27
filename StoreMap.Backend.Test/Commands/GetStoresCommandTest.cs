using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Requests;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Test.Commands
{
    public class GetStoresCommandTest
    {
        private Mock<IStoreRepository> storeRepositoryMock = new Mock<IStoreRepository>();
        private GetStoresCommand command;
        
        [OneTimeSetUp]
        public void Setup()
        {
            command = new GetStoresCommand(storeRepositoryMock.Object);
        }

        [Test]
        public void GetStoresCommand_ShouldGetAllData()
        {
            var store = new Store
            {
                Id = Guid.NewGuid(),
                Name = "test store",
                StoreItems = new List<StoreItem>{new StoreItem()},
                StoreObjects = new List<StoreObject>{new StoreObjectCircle()}
            };
            storeRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<Store>{store});

            var result = command.Execute(new SearchRequest()).Result;

            result.Should().NotBeNull();
            result.Success.Should().Be(true);
            result.Data.Should().NotBeNullOrEmpty();
            
            var resStore = result.Data.First();
            resStore.Id.Should().Be(store.Id);
            resStore.Name.Should().Be(store.Name);
            resStore.StoreItems.Should().NotBeNullOrEmpty();
            resStore.StoreObjects.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GetStoresCommand_ShouldFilterBySearchTerm()
        {
            var store = new Store
            {
                Id = Guid.NewGuid(),
                Name = "test store",
                StoreItems = new List<StoreItem>{new StoreItem()},
                StoreObjects = new List<StoreObject>{new StoreObjectCircle()}
            };
            var searchTerm = "test search term";
            storeRepositoryMock
                .Setup(x => x.GetAll(searchTerm))
                .ReturnsAsync(new List<Store>{store});

            var result = command.Execute(new SearchRequest
            {
                SearchTerm = searchTerm
            }).Result;

            result.Should().NotBeNull();
            result.Success.Should().Be(true);
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.First().Id.Should().Be(store.Id);
        }
    }
}