using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using StoreMap.Components;
using StoreMap.Data.Dtos;
using StoreMap.Data.Enums;
using StoreMap.Logic.Interfaces;
using StoreMap.Pages;
using StoreMap.Test.Base;

namespace StoreMap.Test.Components
{
    public class StoreItemListTest : TestBase
    {
        private readonly Mock<IStoreService> storeServiceMock = new Mock<IStoreService>();
        private readonly Mock<IBrowserService> browserServiceMock = new Mock<IBrowserService>();
        
        private readonly StoreItemExtendedDto testStoreItem = new StoreItemExtendedDto
        {
            Id = Guid.NewGuid(),
            Name = "TestName",
            TempName = "TestTempName",
            X = 123456,
            Y = 654321
        };
        
        [OneTimeSetUp]
        public void Setup()
        {
            Services.AddSingleton(browserServiceMock.Object);
            Services.AddSingleton(storeServiceMock.Object);
        }
        
        private IRenderedComponent<StoreItemList> Render(bool singleItem = false)
        {
            var editStore = RenderComponent<EditStore>().Instance;
            editStore.Store = new StoreDto();
            editStore.StoreItems = new List<StoreItemExtendedDto>{testStoreItem};
            if (!singleItem)
            {
                editStore.StoreItems.Add(new StoreItemExtendedDto());
            }
            
            return RenderComponent<StoreItemList>(x => x.Add(editStore));
        }

        [Test]
        public void StoreItemList_WhenIsEditing_ShouldRenderCorrectItems()
        {
            testStoreItem.IsEditing = true;

            var component = Render(true);

            component.Find(".ant-input");
            component.Markup.Contains(testStoreItem.X.ToString()).Should().BeTrue();
            component.Markup.Contains(testStoreItem.Y.ToString()).Should().BeTrue();
            component.Markup.Contains("Save").Should().BeTrue();
            component.Markup.Contains("Cancel").Should().BeTrue();
            component.Markup.Contains("Edit").Should().BeFalse();
            component.Markup.Contains("Move").Should().BeFalse();
            component.Markup.Contains("Delete").Should().BeFalse();
        }

        [Test]
        public void StoreItemList_WhenNotEditing_ShouldRenderCorrectItems()
        {
            testStoreItem.IsEditing = false;

            var component = Render(true);

            component.Markup.Contains($"<span>{testStoreItem.Name}</span>").Should().BeTrue();
            component.Markup.Contains(testStoreItem.X.ToString()).Should().BeTrue();
            component.Markup.Contains(testStoreItem.Y.ToString()).Should().BeTrue();
            component.Markup.Contains("Save").Should().BeFalse();
            component.Markup.Contains("Cancel").Should().BeFalse();
            component.Markup.Contains("Edit").Should().BeTrue();
            component.Markup.Contains("Move").Should().BeTrue();
            component.Markup.Contains("Delete").Should().BeTrue();
        }

        [Test]
        public void StoreItemList_Delete_ShouldDelete()
        {
            var component = Render();
            
            component.Find(".store-item-list-delete-btn").Click();

            component.Instance.EditStore.StoreItems.Should().NotContain(x => x.Id == testStoreItem.Id);
        }

        [Test]
        public void StoreItemList_Move_ShouldMarkAsMoving()
        {
            testStoreItem.IsEditing = false;
            var component = Render();
            
            component.Find(".store-item-list-move-btn").Click();

            component.Instance.EditStore.StoreItems.First(x => x.Id == testStoreItem.Id).IsMoving.Should().BeTrue();
            component.Instance.EditStore.MapAction.Should().Be(ShapeObjectAction.Move);
        }

        [Test]
        public void StoreItemList_Save_ShouldEndEditingAndSave()
        {
            testStoreItem.IsEditing = true;
            var tempName = testStoreItem.TempName;

            var component = Render();
            
            component.Find(".store-item-list-save-btn").Click();
                
            var updatedItem = component.Instance.EditStore.StoreItems.First(x => x.Id == testStoreItem.Id);
            updatedItem.IsEditing.Should().BeFalse();
            updatedItem.Name.Should().Be(tempName);
        }

        [Test]
        public void StoreItemList_Cancel_ShouldEndEditingAndDiscardChanges()
        {
            testStoreItem.IsEditing = true;
            var name = testStoreItem.Name;

            var component = Render();
            
            component.Find(".store-item-list-cancel-btn").Click();
                
            var updatedItem = component.Instance.EditStore.StoreItems.First(x => x.Id == testStoreItem.Id);
            updatedItem.IsEditing.Should().BeFalse();
            updatedItem.Name.Should().Be(name);
        }

        [Test]
        public void StoreItemList_Cancel_WhenItemIsNew_ShouldDelete()
        {
            testStoreItem.IsEditing = true;
            testStoreItem.Name = null;

            var component = Render();
            
            component.Find(".store-item-list-cancel-btn").Click();
                
            component.Instance.EditStore.StoreItems.Should().NotContain(x => x.Id == testStoreItem.Id);
        }

        [Test]
        public void StoreItemList_Edit_ShouldMarkForEditing()
        {
            testStoreItem.IsEditing = false;

            var component = Render();
            
            component.Find(".store-item-list-edit-btn").Click();
                
            var updatedItem = component.Instance.EditStore.StoreItems.First(x => x.Id == testStoreItem.Id);
            updatedItem.IsEditing.Should().BeTrue();
            updatedItem.TempName.Should().Be(testStoreItem.Name);
        }
    }
}