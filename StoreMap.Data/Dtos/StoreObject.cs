using System.Threading.Tasks;

namespace StoreMap.Data.Dtos
{
    public abstract class StoreObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; } = "#f8f1df";

        public abstract string GetShapeStyles { get; }

        public abstract void UpdateSize(int currentX, int currentY);
    }
}