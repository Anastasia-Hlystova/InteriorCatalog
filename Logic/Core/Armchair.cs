using System;
using System.Xml.Serialization;

namespace Model.Core
{
    [Serializable]
    public class Armchair : Chair
    {
        public bool IsReclining { get; set; } // Может ли откидываться
        public bool HasMassage { get; set; }  // Есть ли функция массажа

        [XmlIgnore]
        public override decimal Price
        {
            get
            {
                decimal price = base.Price; // Базовая цена с учетом свойств Chair
                if (IsReclining) price += BasePrice * 0.3m;
                if (HasMassage) price += BasePrice * 0.5m;
                return price;
            }
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + $" (Armchair, Reclining: {IsReclining}, Massage: {HasMassage})";
        }
    }
}