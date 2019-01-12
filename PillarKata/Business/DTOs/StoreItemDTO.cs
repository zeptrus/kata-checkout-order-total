using Business.Enums;

namespace Business.DTOs
{
    public class StoreItemDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public ItemTypeEnum Type { get; set; }
    }
}
