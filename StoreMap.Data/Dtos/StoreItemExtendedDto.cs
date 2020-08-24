namespace StoreMap.Data.Dtos
{
    public class StoreItemExtendedDto : StoreItemDto
    {
        public string TempName { get; set; }
        public bool IsEditing { get; set; }
        public bool IsMoving { get; set; }
    }
}