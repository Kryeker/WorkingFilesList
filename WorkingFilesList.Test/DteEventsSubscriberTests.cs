﻿// WorkingFilesList
// Visual Studio extension tool window that shows a selectable list of files
// that are open in the editor
// Copyright(C) 2016 Anthony Fung

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program. If not, see<http://www.gnu.org/licenses/>.

using EnvDTE;
using EnvDTE80;
using Moq;
using NUnit.Framework;
using WorkingFilesList.Interface;
using WorkingFilesList.Model;
using WorkingFilesList.Service;

namespace WorkingFilesList.Test
{
    [TestFixture]
    public class DteEventsSubscriberTests
    {
        [Test]
        public void SubscribeToReturnsServicesObject()
        {
            // Arrange

            var events2Mock = new Mock<Events2>();

            events2Mock
                .Setup(e => e.ProjectItemsEvents)
                .Returns(Mock.Of<ProjectItemsEvents>());

            events2Mock
                .Setup(e => e.get_WindowEvents(It.IsAny<Window>()))
                .Returns(Mock.Of<WindowEvents>());

            var servicesToReturn = new DteEventsServices();

            var factoryMock = new Mock<IDteEventsServicesFactory>();
            factoryMock
                .Setup(f => f.CreateDteEventsServices())
                .Returns(servicesToReturn);

            var subscriber = new DteEventsSubscriber(factoryMock.Object);

            // Act

            var returnedServices = subscriber.SubscribeTo(events2Mock.Object);

            // Assert

            factoryMock.VerifyAll();

            Assert.That(returnedServices, Is.EqualTo(servicesToReturn));
        }

        [Test]
        public void SubscribesToWindowActivated()
        {
            // Arrange

            var windowEventsMock = new Mock<WindowEvents>();

            var events2Mock = new Mock<Events2>();

            events2Mock
                .Setup(e => e.ProjectItemsEvents)
                .Returns(Mock.Of<ProjectItemsEvents>());

            events2Mock
                .Setup(e => e.get_WindowEvents(It.IsAny<Window>()))
                .Returns(windowEventsMock.Object);

            var factoryMock = new Mock<IDteEventsServicesFactory>();
            factoryMock.Setup(f => f
                .CreateDteEventsServices()
                .WindowEventsService
                .WindowActivated(
                    It.IsAny<Window>(),
                    It.IsAny<Window>()));

            var subscriber = new DteEventsSubscriber(factoryMock.Object);
            subscriber.SubscribeTo(events2Mock.Object);

            // Act

            windowEventsMock.Raise(w =>
                w.WindowActivated += null,
                null, null);

            // Assert

            factoryMock.Verify(f => f
                .CreateDteEventsServices()
                .WindowEventsService
                .WindowActivated(
                    It.IsAny<Window>(),
                    It.IsAny<Window>()));
        }

        [Test]
        public void SubscribesToWindowCreated()
        {
            // Arrange

            var windowEventsMock = new Mock<WindowEvents>();

            var events2Mock = new Mock<Events2>();

            events2Mock
                .Setup(e => e.ProjectItemsEvents)
                .Returns(Mock.Of<ProjectItemsEvents>());

            events2Mock
                .Setup(e => e.get_WindowEvents(It.IsAny<Window>()))
                .Returns(windowEventsMock.Object);

            var factoryMock = new Mock<IDteEventsServicesFactory>();
            factoryMock.Setup(f => f
                .CreateDteEventsServices()
                .WindowEventsService
                .WindowCreated(It.IsAny<Window>()));

            var subscriber = new DteEventsSubscriber(factoryMock.Object);
            subscriber.SubscribeTo(events2Mock.Object);

            // Act

            windowEventsMock.Raise(w =>
                w.WindowCreated += null,
                Mock.Of<Window>());

            // Assert

            factoryMock.Verify(f => f
                .CreateDteEventsServices()
                .WindowEventsService
                .WindowCreated(It.IsAny<Window>()));
        }

        [Test]
        public void SubscribesToWindowClosing()
        {
            // Arrange

            var windowEventsMock = new Mock<WindowEvents>();

            var events2Mock = new Mock<Events2>();

            events2Mock
                .Setup(e => e.ProjectItemsEvents)
                .Returns(Mock.Of<ProjectItemsEvents>());

            events2Mock
                .Setup(e => e.get_WindowEvents(It.IsAny<Window>()))
                .Returns(windowEventsMock.Object);

            var factoryMock = new Mock<IDteEventsServicesFactory>();
            factoryMock.Setup(f => f
                .CreateDteEventsServices()
                .WindowEventsService
                .WindowClosing(It.IsAny<Window>()));

            var subscriber = new DteEventsSubscriber(factoryMock.Object);
            subscriber.SubscribeTo(events2Mock.Object);

            // Act

            windowEventsMock.Raise(w =>
                w.WindowClosing += null,
                Mock.Of<Window>());

            // Assert

            factoryMock.Verify(f => f
                .CreateDteEventsServices()
                .WindowEventsService
                .WindowClosing(It.IsAny<Window>()));
        }

        [Test]
        public void SubscribesToItemRenamed()
        {
            // Arrange

            var projectItemsEventsMock = new Mock<ProjectItemsEvents>();

            var events2Mock = new Mock<Events2>();

            events2Mock
                .Setup(e => e.ProjectItemsEvents)
                .Returns(projectItemsEventsMock.Object);

            events2Mock
                .Setup(e => e.get_WindowEvents(It.IsAny<Window>()))
                .Returns(Mock.Of<WindowEvents>());

            var factoryMock = new Mock<IDteEventsServicesFactory>();
            factoryMock.Setup(f => f
                .CreateDteEventsServices()
                .ProjectItemsEventsService
                .ItemRenamed(
                    It.IsAny<ProjectItem>(),
                    It.IsAny<string>()));

            var subscriber = new DteEventsSubscriber(factoryMock.Object);
            subscriber.SubscribeTo(events2Mock.Object);

            // Act

            projectItemsEventsMock.Raise(w =>
                w.ItemRenamed += null,
                Mock.Of<ProjectItem>(), string.Empty);

            // Assert

            factoryMock.Verify(f => f
                .CreateDteEventsServices()
                .ProjectItemsEventsService
                .ItemRenamed(
                    It.IsAny<ProjectItem>(),
                    It.IsAny<string>()));
        }
    }
}
