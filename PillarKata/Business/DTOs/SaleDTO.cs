using Business.Enums;

namespace Business.DTOs
{
    public class SaleDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int AmountNeeded { get; set; }
        public int Limit { get; set; }
        public string PreReq { get; set; }
        public PriceTypeEnum PriceType { get; set; }
    }
}