using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Model.Core
{
    [Serializable]
    [XmlInclude(typeof(Chair))]
    [XmlInclude(typeof(Table))]
    [XmlInclude(typeof(Sofa))]
    [XmlInclude(typeof(Bed))]
    public abstract class Furniture
    {
        public Guid UniqueCode { get; set; } = Guid.NewGuid();
        public string ArticleNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        [XmlIgnore]
        public abstract decimal Price { get; }

        public virtual string GetFullInfo()
        {
            return $"{Brand} {Model} - {ArticleNumber}";
        }
    }
}