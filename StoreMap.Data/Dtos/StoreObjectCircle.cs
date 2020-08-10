namespace StoreMap.Data.Dtos
{
    public class StoreObjectCircle : StoreObject
    {
        public double Radius { get; set; }

        public override string GetShapeStyles => $"width: {Radius*2}px; height: {Radius*2}px; border-radius: {Radius}px;";
        public override void UpdateSize(int currentX, int currentY)
        {
            throw new System.NotImplementedException();
        }
    }
}