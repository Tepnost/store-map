namespace StoreMap.Data.Dtos
{
    public class StoreObjectSquare : StoreObject
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public override string GetShapeStyles => $"width: {Width}px; height: {Height}px;";
        
        public override void UpdateSize(int currentX, int currentY)
        {
            Width = currentX - X;
            Height = currentY - Y;
        }
    }
}