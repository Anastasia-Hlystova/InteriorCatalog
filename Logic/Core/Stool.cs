using System;
using System.Xml.Serialization;

namespace Model.Core
{
    [Serializable]
    public class Stool : Chair
    {
        public bool IsBarStool { get; set; } // Высокий барный табурет
        public bool HasBackrest { get; set; } // Есть ли спинка
        public int Height { get; set; } // Высота в см

        [XmlIgnore]
        public override decimal Price
        {
            get
            {
                decimal price = base.Price; // Базовая цена с учетом свойств Chair
                if (IsBarStool) price += BasePrice * 0.25m;
                if (HasBackrest) price += BasePrice * 0.15m;
                price += (Height - 45) * 0.1m; // За каждые см выше стандартных 45 см
                return price;
            }
        }

        public override string GetFullInfo()
        {
            return base.GetFullInfo() + $" (Stool, Bar: {IsBarStool}, Backrest: {HasBackrest}, Height: {Height}cm)";
        }
    }
}