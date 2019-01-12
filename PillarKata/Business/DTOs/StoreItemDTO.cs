using Business.Enums;
using System;

namespace Business.DTOs
{
    public class StoreItemDTO : ICloneable
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public ItemTypeEnum Type { get; set; }
        public double Amount { get; internal set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
