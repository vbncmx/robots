namespace Robots.Tests
{
    using System;

    using NUnit.Framework;

    public class LrfCommandFactoryTests
    {
        private LrfCommandFactory _factory;

        private static readonly char[] SupportedChars = new[] { 'l', 'L', 'r', 'R', 'f', 'F' };

        private static readonly char[] OtherChars = new[] { 'a', 'b', 'c', 'd', 'e' };

        [SetUp]
        public void Setup()
        {
            _factory = new LrfCommandFactory();
        }

        [TestCaseSource(nameof(SupportedChars))]
        public void IsSupportedReturnsTrueOnLrfChars(char c)
        {
            var isSupported = _factory.IsSupported(c);
            Assert.IsTrue(isSupported);
        }

        [TestCaseSource(nameof(OtherChars))]
        public void IsSupportedReturnsFalseOnOtherChars(char c)
        {
            var isSupported = _factory.IsSupported(c);
            Assert.IsFalse(isSupported);
        }

        [TestCaseSource(nameof(SupportedChars))]
        public void ConstructReturnsNotNullCommandOnSupportedChars(char c)
        {
            var command = _factory.GetCommand(c);
            Assert.IsNotNull(command);
        }

        [TestCaseSource(nameof(OtherChars))]
        public void ConstructThrowsOnOtherChars(char c)
        {
            Assert.Throws<ArgumentException>(() =>_factory.GetCommand(c));
        }
    }
}