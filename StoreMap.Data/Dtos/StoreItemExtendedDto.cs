using Newtonsoft.Json;

namespace StoreMap.Data.Dtos
{
    public class StoreItemExtendedDto : StoreItemDto
    {
        public StoreItemExtendedDto() {}
        
        public StoreItemExtendedDto(StoreItemDto item) : base(item) {}
        
        [JsonIgnore]
        public string TempName { get; set; }
        [JsonIgnore]
        public bool IsEditing { get; set; }
        [JsonIgnore]
        public bool IsMoving { get; set; }
        [JsonIgnore]
        public bool IsFocused { get; set; }
    }
}