using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using StoreMap.Components.Map;
using StoreMap.Data.Dtos;
using StoreMap.Data.Enums;
using StoreMap.Logic.ServiceContracts;
using StoreMap.Pages;
using StoreMap.Test.Base;

namespace StoreMap.Test.Components
{
    public class MapEditTest : TestBase
    {
        private readonly Mock<IStoreService> storeServiceMock = new Mock<IStoreService>();
        private readonly Mock<IBrowserService> browserServiceMock = new Mock<IBrowserService>();
        private readonly StoreObjectRectDto testStoreObject = new StoreObjectRectDto
        {
            Width = 20,
            Height = 20,
            X = 1,
            Y = 1
        };
        private readonly RectPosition mapPosition = new RectPosition
        {
            Top = 10,
            Bottom = 110,
            Left = 10,
            Right = 110
        };
        
        [OneTimeSetUp]
        public void Setup()
        {
            browserServiceMock
                .Setup(x => x.GetElementPosition(It.IsAny<string>()))
                .Returns(Task.FromResult(mapPosition));

            Services.AddSingleton(browserServiceMock.Object);
            Services.AddSingleton(storeServiceMock.Object);
        }
        
        private IRenderedComponent<MapEdit> Render(bool hasShape = false)
        {
            var editStore = RenderComponent<EditStore>().Instance;
            editStore.Store = new StoreDto
            {
                StoreObjects = new List<StoreObjectDto>()
            };
            editStore.StoreItems = new List<StoreItemExtendedDto>();
            if (hasShape)
            {
                editStore.Store.StoreObjects.Add(testStoreObject);
            }
            
            return RenderComponent<MapEdit>(x => x.Add(editStore));
        }

        [TestCase(1, 1, false)]
        [TestCase(1, 11, false)]
        [TestCase(11, 1, false)]
        [TestCase(12, 12, true)]
        [TestCase(20, 20, true)]
        public void MapEdit_Delete_ShouldDeleteObject(int clickX, int clickY, bool shouldDelete)
        {
            var component = Render(true);
            component.Instance.EditStore.MapAction = ShapeObjectAction.Delete;
            
            component.Find("#map-drawing").Click(new MouseEventArgs
            {
                ClientX = clickX,
                ClientY = clickY
            });

            var item = component.Instance.EditStore.Store.StoreObjects.FirstOrDefault();

            if (shouldDelete)
            {
                item.Should().BeNull();
            }
            else
            {
                item.Should().NotBeNull();
            }
        }

        [Test]
        public void MapEdit_ShouldDrawShape_Rect()
        {
            var component = Render();
            component.Instance.EditStore.MapAction = ShapeObjectAction.Create;
            component.Find(".map-edit-controls-shape-rect").Click();
            
            component.Find("#map-drawing").Click(new MouseEventArgs
            {
                ClientX = 11,
                ClientY = 11
            });
            component.Find("#map-drawing").MouseMove(new MouseEventArgs
            {
                ClientX = 21,
                ClientY = 21
            });
            component.Find("#map-drawing").Click();
            
            var item = component.Instance.EditStore.Store.StoreObjects.FirstOrDefault();
            item.Should().NotBeNull();
            item.Should().BeOfType<StoreObjectRectDto>();
            var rect = (StoreObjectRectDto) item;
            rect.X.Should().Be(1);
            rect.Y.Should().Be(1);
            rect.Width.Should().Be(10);
            rect.Height.Should().Be(10);
        }

        [Test]
        public void MapEdit_ShouldDrawShape_Circle()
        {
            var component = Render();
            component.Instance.EditStore.MapAction = ShapeObjectAction.Create;
            component.Find(".map-edit-controls-shape-circle").Click();
            
            component.Find("#map-drawing").Click(new MouseEventArgs
            {
                ClientX = 20,
                ClientY = 20
            });
            component.Find("#map-drawing").MouseMove(new MouseEventArgs
            {
                ClientX = 20,
                ClientY = 25
            });
            component.Find("#map-drawing").Click();
            
            var item = component.Instance.EditStore.Store.StoreObjects.FirstOrDefault();
            item.Should().NotBeNull();
            item.Should().BeOfType<StoreObjectCircleDto>();
            var circle = (StoreObjectCircleDto) item;
            circle.X.Should().Be(10);
            circle.Y.Should().Be(10);
            circle.Diameter.Should().Be(10);
        }

        [Test]
        public void MapEdit_ShouldCreateStoreItem()
        {
            var component = Render();
            component.Instance.EditStore.MapAction = ShapeObjectAction.Create;
            component.Find(".map-edit-controls-shape-item").Click();

            component.Find("#map-drawing").Click(new MouseEventArgs
            {
                ClientX = 11,
                ClientY = 11
            });
            
            var item = component.Instance.EditStore.Store.StoreItems.FirstOrDefault();
            item.Should().NotBeNull();
            item.Should().BeOfType<StoreItemExtendedDto>();
            var storeItem = (StoreItemExtendedDto) item;
            storeItem.X.Should().Be(1);
            storeItem.Y.Should().Be(1);
            storeItem.IsEditing.Should().BeTrue();
        }

        [Test]
        public void MapEdit_Move_ShouldMoveStoreItem()
        {
            var component = Render();
            component.Instance.EditStore.MapAction = ShapeObjectAction.Move;
            component.Instance.EditStore.StoreItems.Add(new StoreItemExtendedDto
            {
                IsMoving = true
            });
            
            component.Find("#map-drawing").Click(new MouseEventArgs
            {
                ClientX = 11,
                ClientY = 11
            });
            
            var item = component.Instance.EditStore.Store.StoreItems.FirstOrDefault();
            item.Should().NotBeNull();
            var storeItem = (StoreItemExtendedDto) item;
            storeItem.X.Should().Be(1);
            storeItem.Y.Should().Be(1);
            storeItem.IsMoving.Should().BeFalse();
            component.Instance.EditStore.MapAction.Should().Be(ShapeObjectAction.Create);
        }
    }
}