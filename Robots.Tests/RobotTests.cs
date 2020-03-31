namespace Robots.Tests
{
    using System;

    using NUnit.Framework;

    public class RobotTests
    {
        private Grid _grid;

        [SetUp]
        public void Setup()
        {
            _grid = Grid.Construct("5 5");
        }

        [Test]
        public void ConstructFailsOnInvalidInput()
        {
            Assert.Throws<ArgumentException>(() => Robot.Construct("xyz", _grid));
        }

        [Test]
        public void ConstructFailsWhenPositionIsOutsideGrid()
        {
            Assert.Throws<ArgumentException>(() => Robot.Construct("1 6 N", _grid));
        }

        [Test]
        public void ConstructSucceedsOnValidInput()
        {
            var robot = Robot.Construct("1 3 E", _grid);

            Assert.AreEqual(1, robot.X);
            Assert.AreEqual(3, robot.Y);
            Assert.AreEqual(RobotStatus.E, robot.Status);
        }

        [Test]
        public void GetPositionReturnsInitialPosition()
        {
            var robot = Robot.Construct("1 3 E", _grid);

            var position = robot.GetPosition();

            Assert.AreEqual(1, position.X);
            Assert.AreEqual(3, position.Y);
        }

        [TestCase("3 3 E", 4, 3)]
        [TestCase("3 3 W", 2, 3)]
        [TestCase("3 3 N", 3, 4)]
        [TestCase("3 3 S", 3, 2)]
        public void NextPositionWorksFine(string robotInput, int expectedX, int expectedY)
        {
            var robot = Robot.Construct(robotInput, _grid);

            var nextPosition = robot.NextPosition();

            Assert.AreEqual(expectedX, nextPosition.X);
            Assert.AreEqual(expectedY, nextPosition.Y);
        }

        [Test]
        public void NextPositionDoesNotChangeRobot()
        {
            var robot = Robot.Construct("2 3 N", _grid);

            var nextPosition = robot.NextPosition();

            Assert.AreEqual(2, robot.X);
            Assert.AreEqual(3, robot.Y);
            Assert.AreEqual(RobotStatus.N, robot.Status);
        }

        [TestCase("3 3 E", 4, 3)]
        [TestCase("3 3 W", 2, 3)]
        [TestCase("3 3 N", 3, 4)]
        [TestCase("3 3 S", 3, 2)]
        public void MoveForwardWorksFine(string robotInput, int expectedX, int expectedY)
        {
            var robot = Robot.Construct(robotInput, _grid);

            robot.MoveForward();

            Assert.AreEqual(expectedX, robot.X);
            Assert.AreEqual(expectedY, robot.Y);
        }
    }
}