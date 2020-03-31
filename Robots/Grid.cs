namespace Robots
{
    using System;
    using System.Collections.Generic;

    using Robots.Interfaces;

    public class Grid : IGrid
    {
        private readonly HashSet<Point> _scentPoints = new HashSet<Point>();

        private uint _width;

        private uint _height;

        private Grid() { }

        public static Grid Construct(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var split = input.Split(' ');

            if (split.Length != 2 || 
                !uint.TryParse(split[0], out var width) || 
                !uint.TryParse(split[1], out var height))
                throw new ArgumentException("Invalid grid input", nameof(input));
            if (width == 0 || width > 50) 
                throw new ArgumentOutOfRangeException(nameof(width), "0 < width <= 50");
            if (height == 0 || height > 50)
                throw new ArgumentOutOfRangeException(nameof(height), "0 < height <= 50");

            var grid = new Grid { _width = width, _height = height };

            return grid;
        }

        public uint Width => _width;

        public uint Height => _height;

        public void AddScent(Point point)
        {
            if (IsOutside(point))
                throw new ArgumentOutOfRangeException(nameof(point), "The point is outside of grid bounds");

            _scentPoints.Add(point);
        }

        public bool IsScent(Point point)
        {
            return _scentPoints.Contains(point);
        }

        public bool IsOutside(Point point)
        {
            return point.X < 0 || point.X > Width || point.Y < 0 || point.Y > Height;
        }
    }
}