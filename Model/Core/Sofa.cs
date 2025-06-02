using System;
using System.Xml.Serialization;
namespace Model.Core
{
    [Serializable]
    public class Sofa : Furniture
    {
        public int NumberOfSeats { get; set; }
        public bool IsModular { get; set; }
        [XmlIgnore]
        public override decimal Price
        {
            get
            {
                decimal price = BasePrice;
                price += BasePrice * 0.1m * (NumberOfSeats - 2); // базовый расчет на 2 места
                if (IsModular) price += BasePrice * 0.3m;
                return price;
            }
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + $" (Sofa, Seats: {NumberOfSeats}, Modular: {IsModular})";
        }
    }
}