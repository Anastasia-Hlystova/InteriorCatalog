using System;
using System.Xml.Serialization;
namespace Model.Core
{
    [Serializable]
    public class Bed : Furniture
    {
        public string Size { get; set; } // "Single", "Double", "King"
        public bool HasStorage { get; set; }
        [XmlIgnore]
        public override decimal Price
        {
            get
            {
                decimal price = BasePrice;
                if (Size == "Double") price += BasePrice * 0.25m;
                if (Size == "King") price += BasePrice * 0.5m;
                if (HasStorage) price += BasePrice * 0.2m;
                return price;
            }
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + $" (Bed, Size: {Size}, Storage: {HasStorage})";
        }
    }
}