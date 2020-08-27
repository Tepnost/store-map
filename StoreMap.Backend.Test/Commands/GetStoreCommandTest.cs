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
    public class GetStoreCommandTest
    {
        private Mock<IStoreRepository> storeRepositoryMock = new Mock<IStoreRepository>();
        private GetStoreCommand command;
        
        [OneTimeSetUp]
        public void Setup()
        {
            command = new GetStoreCommand(storeRepositoryMock.Object);
        }

        [Test]
        public void GetStoreCommand_ShouldGetAllData()
        {
            var item = new StoreItem
            {
                Id = Guid.NewGuid(),
                Name = "test store item",
                X = 1,
                Y = 2
            };
            var rect = new StoreObjectRect
            {
                X = 1,
                Y = 2,
                Color = "test color",
                Height = 3,
                Width = 4
            };
            var circle = new StoreObjectCircle
            {
                Color = "test color 2",
                Diameter = 1,
                X = 2,
                Y = 3
            };
            var store = new Store
            {
                Id = Guid.NewGuid(),
                Name = "test store",
                StoreItems = new List<StoreItem>{item},
                StoreObjects = new List<StoreObject>{rect, circle}
            };
            storeRepositoryMock
                .Setup(x => x.FindOne(store.Id))
                .ReturnsAsync(store);

            var result = command.Execute(new GetByGuidRequest
            {
                Id = store.Id
            }).Result;

            result.Should().NotBeNull();
            result.Success.Should().Be(true);
            result.Data.Id.Should().Be(store.Id);
            result.Data.Name.Should().Be(store.Name);
            result.Data.StoreItems.Should().NotBeNullOrEmpty();
            result.Data.StoreObjects.Should().NotBeNullOrEmpty();
            var resItem = result.Data.StoreItems.First();
            resItem.Id.Should().Be(item.Id);
            resItem.Name.Should().Be(item.Name);
            resItem.X.Should().Be(item.X);
            resItem.Y.Should().Be(item.Y);
            
            var resRect = (StoreObjectRectDto)result.Data.StoreObjects.First(x => x is StoreObjectRectDto);
            resRect.Color.Should().Be(rect.Color);
            resRect.Width.Should().Be(rect.Width);
            resRect.Height.Should().Be(rect.Height);
            resRect.X.Should().Be(rect.X);
            resRect.Y.Should().Be(rect.Y);
            
            var resCircle = (StoreObjectCircleDto)result.Data.StoreObjects.First(x => x is StoreObjectCircleDto);
            resCircle.Color.Should().Be(circle.Color);
            resCircle.Diameter.Should().Be(circle.Diameter);
            resCircle.X.Should().Be(circle.X);
            resCircle.Y.Should().Be(circle.Y);
        }

        [Test]
        public void GetStoreCommand_ShouldCatchExceptions()
        {
            storeRepositoryMock
                .Setup(x => x.FindOne(It.IsAny<Guid>()))
                .Throws<Exception>();

            var result = command.Execute(new GetByGuidRequest()).Result;

            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().NotBeNullOrEmpty();
        }
    }
}