using System;
using System.Xml.Serialization;
namespace Model.Core
{
    [Serializable]
    public class Chair : Furniture
    {
        public bool HasArmrests { get; set; }
        public string Material { get; set; } // "Wood", "Metal", "Plastic"
        [XmlIgnore]
        public override decimal Price
        {
            get
            {
                decimal price = BasePrice;
                if (HasArmrests) price += BasePrice * 0.15m;
                if (Material == "Wood") price += BasePrice * 0.2m;
                return price;
            }
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + $" (Chair, Material: {Material}, Armrests: {HasArmrests})";
        }
    }
}