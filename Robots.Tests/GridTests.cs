using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    public class GridTests
    {
        [Test]
        public void ConstructThrowsOnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => Grid.Construct(null));
        }

        [Test]
        public void ConstructThrowsOnInvalidInput()
        {
            Assert.Throws<ArgumentException>(() => Grid.Construct("xyz"));
        }

        [Test]
        public void ConstructThrowsOnTooLargeWidth()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Grid.Construct("100 1"));
        }

        [Test]
        public void ConstructThrowsOnTooLargeHeight()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Grid.Construct("1 100"));
        }

        [Test]
        public void ConstructSucceedsOnValidInput()
        {
            var grid = Grid.Construct("5 5");

            Assert.AreEqual(5, grid.Width);
            Assert.AreEqual(5, grid.Height);
        }

        [Test]
        public void IsOutsideTrueOnOutsidePoint()
        {
            var grid = Grid.Construct("5 5");

            var point = new Point(0, 6);

            var isOutside = grid.IsOutside(point);

            Assert.IsTrue(isOutside);
        }

        [Test]
        public void IsOutsideFalseOnInsidePoint()
        {
            var grid = Grid.Construct("5 5");

            var point = new Point(3, 3);

            var isOutside = grid.IsOutside(point);

            Assert.IsFalse(isOutside);
        }

        [Test]
        public void IsScentFalseWhenScentIsNotAdded()
        {
            var grid = Grid.Construct("5 5");

            var point = new Point(5, 5);

            var isScent = grid.IsScent(point);

            Assert.IsFalse(isScent);
        }

        [Test]
        public void AddScentThrowsOnOutsidePoint()
        {
            var grid = Grid.Construct("5 5");

            var point = new Point(0,6);

            Assert.Throws<ArgumentOutOfRangeException>(() => grid.AddScent(point));
        }

        [Test]
        public void IsScentTrueAfterScentIsAdded()
        {
            var grid = Grid.Construct("5 5");

            var point = new Point(5, 5);

            grid.AddScent(point);

            var isScent = grid.IsScent(point);

            Assert.IsTrue(isScent);
        }
    }
}