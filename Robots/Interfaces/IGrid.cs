namespace Robots.Interfaces
{
    public interface IGrid
    {
        uint Width { get; }

        uint Height { get; }

        void AddScent(Point point);

        bool IsScent(Point point);

        bool IsOutside(Point point);
    }
}