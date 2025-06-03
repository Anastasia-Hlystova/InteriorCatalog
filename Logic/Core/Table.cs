using System;
using System.Xml.Serialization;
namespace Model.Core
{
    [Serializable]
    public class Table : Furniture
    {
        public string Shape { get; set; } // "Round", "Square", "Rectangle"
        public int NumberOfLegs { get; set; }
        [XmlIgnore]
        public override decimal Price
        {
            get
            {
                decimal price = BasePrice;
                if (Shape == "Round") price += BasePrice * 0.1m;
                if (NumberOfLegs > 4) price += BasePrice * 0.05m * (NumberOfLegs - 4);
                return price;
            }
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + $" (Table, Shape: {Shape}, Legs: {NumberOfLegs})";
        }
    }
}