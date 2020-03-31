namespace Robots.Tests
{
    using System;

    using Moq;

    using NUnit.Framework;

    using Robots.Command;
    using Robots.Interfaces;

    public class LeftCommandTests
    {
        private LeftCommand _command;

        private Mock<IGrid> _gridMock;

        private IRobot _robot;

        [SetUp]
        public void Setup()
        {
            _command = new LeftCommand();

            _gridMock = new Mock<IGrid>();
            _gridMock.Setup(x => x.Width).Returns(5);
            _gridMock.Setup(x => x.Height).Returns(5);

            _robot = Robot.Construct("3 3 N", _gridMock.Object);
        }

        [Test]
        public void ThrowsOnNullRobot()
        {
            Assert.Throws<ArgumentNullException>(() => _command.ExecuteAsync(_gridMock.Object, null));
        }

        [Test]
        public void DoesNotChangeRobotCoordinates()
        {
            _command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            Assert.AreEqual(3, _robot.X);
            Assert.AreEqual(3, _robot.Y);
        }
        
        [TestCase(1, RobotStatus.W)]
        [TestCase(2, RobotStatus.S)]
        [TestCase(3, RobotStatus.E)]
        public void ChangesRobotDirectionCorrectly(int times, RobotStatus expectedStatus)
        {
            for (var i = 0; i < times; i++)
            {
                _command.ExecuteAsync(_gridMock.Object, _robot);
            }

            Assert.AreEqual(expectedStatus, _robot.Status);
        }
    }
}