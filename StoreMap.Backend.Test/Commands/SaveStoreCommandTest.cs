using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Test.Commands
{
    public class SaveStoreCommandTest
    {
        private Mock<IStoreRepository> storeRepositoryMock = new Mock<IStoreRepository>();
        private SaveStoreCommand command;
        
        [OneTimeSetUp]
        public void Setup()
        {
            storeRepositoryMock
                .Setup(x => x.Insert(It.IsAny<Store>()))
                .ReturnsAsync((Store x) => x);
            storeRepositoryMock
                .Setup(x => x.Update(It.IsAny<Store>()))
                .ReturnsAsync((Store x) => x);
            
            command = new SaveStoreCommand(storeRepositoryMock.Object);
        }
        
        [Test]
        public void SaveStoreCommand_WhenNew_ShouldInsert()
        {
            var item = new StoreItemDto
            {
                Id = Guid.NewGuid(),
                Name = "test store item",
                X = 1,
                Y = 2
            };
            var rect = new StoreObjectRectDto
            {
                X = 1,
                Y = 2,
                Color = "test color",
                Height = 3,
                Width = 4
            };
            var circle = new StoreObjectCircleDto
            {
                Color = "test color 2",
                Diameter = 1,
                X = 2,
                Y = 3
            };
            var store = new StoreDto
            {
                Id = Guid.NewGuid(),
                Name = "test store",
                StoreItems = new List<StoreItemDto>{item},
                StoreObjects = new List<StoreObjectDto>{rect, circle}
            };
            
            var result = command.Execute(store).Result;

            result.Success.Should().BeTrue();
            result.Message.Should().NotBeNullOrEmpty();
            result.Data.Should().NotBeNull();
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
        public void SaveStoreCommand_WhenHasGuid_ShouldUpdate()
        {
            var request = new StoreDto
            {
                Id = Guid.NewGuid()
            };
            
            var result = command.Execute(request).Result;

            result.Success.Should().BeTrue();
            result.Message.Should().NotBeNullOrEmpty();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().Be(request.Id);
        }
    }
}