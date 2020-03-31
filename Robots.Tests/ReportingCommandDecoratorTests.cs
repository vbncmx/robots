namespace Robots.Tests
{
    using System;

    using Moq;

    using NUnit.Framework;

    using Robots.Command;
    using Robots.Interfaces;

    public class ReportingCommandDecoratorTests
    {
        private Mock<ICommand> _baseCommandMock;

        private Mock<IReporter> _reporterMock;

        private Mock<IGrid> _gridMock;

        private Mock<IRobot> _robotMock;

        private ReportingCommandDecorator _decorator;

        [SetUp]
        public void Setup()
        {
            _baseCommandMock = new Mock<ICommand>();
            _reporterMock = new Mock<IReporter>();
            _gridMock = new Mock<IGrid>();
            _robotMock = new Mock<IRobot>();
            _decorator = new ReportingCommandDecorator(_baseCommandMock.Object, _reporterMock.Object);
        }

        [Test]
        public void ThrowsOnNullBaseCommand()
        {
            Assert.Throws<ArgumentNullException>(() => new ReportingCommandDecorator(null, _reporterMock.Object));
        }

        [Test]
        public void ThrowsOnNullGrid()
        {
            Assert.Throws<ArgumentNullException>(() => new ReportingCommandDecorator(_baseCommandMock.Object, null));
        }

        [Test]
        public void CallsBaseCommandOnExecute()
        {
            _decorator.ExecuteAsync(_gridMock.Object, _robotMock.Object).Wait();

            _baseCommandMock.Verify(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>()), Times.Once);
        }

        [Test]
        public void CallsReporterOnExecute()
        {
            _decorator.ExecuteAsync(_gridMock.Object, _robotMock.Object).Wait();

            _reporterMock.Verify(x => x.ReportAsync(It.IsAny<IRobot>()), Times.Once);
        }

        [Test]
        public void DoesNotSupressExceptionInBaseCommand()
        {
            _baseCommandMock.Setup(x => x.ExecuteAsync(It.IsAny<IGrid>(), It.IsAny<IRobot>()))
                .Throws(new TestException());
            try
            {
                _decorator.ExecuteAsync(_gridMock.Object, _robotMock.Object).Wait();
            }
            catch (Exception exc)
            {
                var innerException = exc.InnerException;
                Assert.IsNotNull(innerException);
                Assert.AreEqual(typeof(TestException), innerException.GetType());
            }
        }

        [Test]
        public void DoesNotSupressExceptionInReporter()
        {
            _reporterMock.Setup(x => x.ReportAsync(It.IsAny<IRobot>())).Throws(new TestException());

            try
            {
                _decorator.ExecuteAsync(_gridMock.Object, _robotMock.Object).Wait();
            }
            catch (Exception exc)
            {
                var innerException = exc.InnerException;
                Assert.IsNotNull(innerException);
                Assert.AreEqual(typeof(TestException), innerException.GetType());
            }
        }

        class TestException : Exception { }
    }
}