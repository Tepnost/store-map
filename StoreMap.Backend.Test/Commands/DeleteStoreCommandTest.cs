using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StoreMap.Backend.Data.Entities;
using StoreMap.Backend.Data.Interfaces;
using StoreMap.Backend.Logic.Commands;
using StoreMap.Backend.Logic.Requests;

namespace StoreMap.Backend.Test.Commands
{
    public class DeleteStoreCommandTest
    {
        private Mock<IStoreRepository> storeRepositoryMock = new Mock<IStoreRepository>();
        private DeleteStoreCommand command;
        
        [OneTimeSetUp]
        public void Setup()
        {
            command = new DeleteStoreCommand(storeRepositoryMock.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DeleteStoreCommand_ShouldHandleRepositoryResponse(bool repositoryResponse)
        {
            storeRepositoryMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(repositoryResponse);

            var result = command.Execute(new GetByGuidRequest()).Result;

            result.Should().NotBeNull();
            result.Success.Should().Be(repositoryResponse);
            result.Data.Should().Be(repositoryResponse);
            result.Message.Should().NotBeNullOrEmpty();
        }
    }
}