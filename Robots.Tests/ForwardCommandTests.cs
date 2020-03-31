namespace Robots.Tests
{
    using System;

    using Moq;

    using NUnit.Framework;

    using Robots.Command;
    using Robots.Interfaces;

    public class ForwardCommandTests
    {
        private IRobot _robot;

        private Mock<IGrid> _gridMock;

        private ForwardCommand _command;

        [SetUp]
        public void Setup()
        {
            _gridMock = new Mock<IGrid>();
            _gridMock.Setup(x => x.Width).Returns(5);
            _gridMock.Setup(x => x.Height).Returns(5);
            _gridMock.Setup(x => x.IsScent(It.IsAny<Point>())).Returns(false);
            _gridMock.Setup(x => x.IsOutside(It.IsAny<Point>())).Returns(false);

            _robot = Robot.Construct("3 3 N", _gridMock.Object);

            _command = new ForwardCommand();
        }

        [Test]
        public void ThrowsOnNullGrid()
        {
            Assert.Throws<ArgumentNullException>(() => _command.ExecuteAsync(null, _robot));
        }

        [Test]
        public void ThrowsOnNullRobot()
        {
            Assert.Throws<ArgumentNullException>(() => _command.ExecuteAsync(_gridMock.Object, null));
        }

        [TestCase("3 3 N", 3, 4)]
        [TestCase("3 3 S", 3, 2)]
        [TestCase("3 3 W", 2, 3)]
        [TestCase("3 3 E", 4, 3)]
        public void MovesRobotCorrectly(string robotInput, int expectedX, int expectedY)
        {
            _robot = Robot.Construct(robotInput, _gridMock.Object);

            _command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            Assert.AreEqual(expectedX, _robot.X);
            Assert.AreEqual(expectedY, _robot.Y);
        }

        [Test]
        public void SetsLostIfMovingOutsideBounds()
        {
            _gridMock.Setup(x => x.IsOutside(It.IsAny<Point>())).Returns(true);

            _command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            Assert.AreEqual(RobotStatus.LOST, _robot.Status);
        }

        [Test]
        public void IgnoresIfRobotIsOnScentAndMovingOutsideBounds()
        {
            _gridMock.Setup(x => x.IsScent(It.IsAny<Point>())).Returns(true);
            _gridMock.Setup(x => x.IsOutside(It.IsAny<Point>())).Returns(true);

            _command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            Assert.AreEqual(3, _robot.X);
            Assert.AreEqual(3, _robot.Y);
        }

        [Test]
        public void MovesRobotIfItIsOnScentButMovingInsideBounds()
        {
            _gridMock.Setup(x => x.IsScent(It.IsAny<Point>())).Returns(true);
            _gridMock.Setup(x => x.IsOutside(It.IsAny<Point>())).Returns(false);

            _command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            Assert.AreEqual(3, _robot.X);
            Assert.AreEqual(4, _robot.Y);
        }
    }
}