namespace Robots.Tests
{
    using System;
    using System.Threading.Tasks;

    using Moq;

    using NUnit.Framework;

    using Robots.Command;
    using Robots.Interfaces;

    public class TextCommandTests
    {
        private Mock<IGrid> _gridMock;

        private Mock<ICommand> _commandMock;

        private Mock<ICommandFactory> _commandFactoryMock;

        private Robot _robot;

        [SetUp]
        public void Setup()
        {
            _commandMock = new Mock<ICommand>();
            _commandMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>())).Returns(Task.FromResult(0));

            _commandFactoryMock = new Mock<ICommandFactory>();
            _commandFactoryMock.Setup(x => x.GetCommand(It.IsAny<char>())).Returns(_commandMock.Object);

            _gridMock = new Mock<IGrid>();
            _gridMock.Setup(x => x.Width).Returns(5);
            _gridMock.Setup(x => x.Height).Returns(5);
            _gridMock.Setup(x => x.IsScent(It.IsAny<Point>())).Returns(false);
            _gridMock.Setup(x => x.IsOutside(It.IsAny<Point>())).Returns(false);

            _robot = Robot.Construct("3 3 N", _gridMock.Object);
        }

        [Test]
        public void ConstructorThrowsOnNullCommandFactory()
        {
            Assert.Throws<ArgumentNullException>(() => new TextCommand("fff", null));
        }

        [Test]
        public void ConstructorThrowsOnNullInstructions()
        {
            Assert.Throws<ArgumentNullException>(() => new TextCommand(null, _commandFactoryMock.Object));
        }

        [Test]
        public void ConstructorThrowsIfInstructionsContainNotSupportedCharacters()
        {
            _commandFactoryMock.Setup(x => x.IsSupported(It.IsAny<char>())).Returns(false);

            Assert.Throws<ArgumentException>(() => new TextCommand("xyz", _commandFactoryMock.Object));
        }

        [Test]
        public void ConstructorDoesNotThrowIfCommandFactorySupportsAllCharacters()
        {
            _commandFactoryMock.Setup(x => x.IsSupported(It.IsAny<char>())).Returns(true);

            Assert.DoesNotThrow(() => new TextCommand("abcdefg", _commandFactoryMock.Object));
        }

        [Test]
        public void ExecutesAllCommandsOneByOne()
        {
            string actualString = "";

            _commandFactoryMock.Setup(x => x.IsSupported(It.IsAny<char>())).Returns(true);

            var aMock = new Mock<ICommand>();
            aMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>())).Callback(() => actualString += "a");
            _commandFactoryMock.Setup(x => x.GetCommand(It.Is<char>(c => c == 'a'))).Returns(aMock.Object);

            var bMock = new Mock<ICommand>();
            bMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>())).Callback(() => actualString += "b");
            _commandFactoryMock.Setup(x => x.GetCommand(It.Is<char>(c => c == 'b'))).Returns(bMock.Object);

            var cMock = new Mock<ICommand>();
            cMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>())).Callback(() => actualString += "c");
            _commandFactoryMock.Setup(x => x.GetCommand(It.Is<char>(c => c == 'c'))).Returns(cMock.Object);

            var command = new TextCommand("abc", _commandFactoryMock.Object);

            command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            Assert.AreEqual("abc", actualString);
        }

        [Test]
        public void StopsExecutionAfterRobotWasLost()
        {
            _commandFactoryMock.Setup(x => x.IsSupported(It.IsAny<char>())).Returns(true);

            var aMock = new Mock<ICommand>();
            aMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>())).Returns(Task.FromResult(0));
            _commandFactoryMock.Setup(x => x.GetCommand(It.Is<char>(c => c == 'a'))).Returns(aMock.Object);

            var bMock = new Mock<ICommand>();
            bMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>()))
                .Callback((IGrid grid, IRobot robot) => robot.Status = RobotStatus.LOST)
                .Returns(Task.FromResult(0));
            _commandFactoryMock.Setup(x => x.GetCommand(It.Is<char>(c => c == 'b'))).Returns(bMock.Object);

            var cMock = new Mock<ICommand>();
            cMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>())).Returns(Task.FromResult(0));
            _commandFactoryMock.Setup(x => x.GetCommand(It.Is<char>(c => c == 'c'))).Returns(cMock.Object);

            var command = new TextCommand("abc", _commandFactoryMock.Object);

            command.ExecuteAsync(_gridMock.Object, _robot).Wait();

            aMock.Verify(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>()), Times.Once);
            bMock.Verify(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>()), Times.Once);
            cMock.Verify(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>()), Times.Never);
        }
    }
}