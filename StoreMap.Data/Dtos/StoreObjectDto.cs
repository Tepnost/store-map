using System.Threading.Tasks;

namespace StoreMap.Data.Dtos
{
    public abstract class StoreObjectDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; } = "#f8f1df";

        public abstract string GetShapeStyles { get; }

        public abstract void UpdateSize(int currentX, int currentY, RectPosition boundaries = null);

        public abstract bool IsInside(int targetX, int targetY);
    }
}