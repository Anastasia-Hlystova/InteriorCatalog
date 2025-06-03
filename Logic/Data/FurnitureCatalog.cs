using Model.Core;
using Model.Data.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Model.Data
{
    [Serializable]
    public partial class FurnitureCatalog : IFurnitureCatalog
    {
        public string Name { get; set; }
        public string Season { get; set; }
        public int Year { get; set; }
        [XmlArray("FurnitureItems")]
        [XmlArrayItem("Chair", typeof(Chair))]
        [XmlArrayItem("Table", typeof(Table))]
        [XmlArrayItem("Sofa", typeof(Sofa))]
        [XmlArrayItem("Bed", typeof(Bed))]
        [XmlArrayItem("Stool", typeof(Stool))]
        [XmlArrayItem("Armchair", typeof(Armchair))]
        public List<Furniture> Items { get; private set; } = new List<Furniture>();

        public void Add(Furniture item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Items.Add(item);
        }

        public void Add(Furniture[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            Items.AddRange(items);
        }

        public bool Remove(string articleNumber)
        {
            var itemToRemove = Items.FirstOrDefault(i => i.ArticleNumber == articleNumber);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
                return true;
            }
            return false;
        }

        public int Remove(Type furnitureType)
        {
            if (!typeof(Furniture).IsAssignableFrom(furnitureType))
                throw new ArgumentException("Type must inherit from Furniture", nameof(furnitureType));

            var itemsToRemove = Items.Where(i => i.GetType() == furnitureType).ToList();
            int count = itemsToRemove.Count;

            foreach (var item in itemsToRemove)
            {
                Items.Remove(item);
            }

            return count;
        }

        public IEnumerable<Furniture> GetSortedByArticleNumber()
        {
            return Items.OrderBy(i => i.ArticleNumber);
        }

        public void SaveToJson(string filePath)
        {
            var serializer = new JsonSerializer<FurnitureCatalog>();
            serializer.SaveToFile(this, filePath);
        }
        public static FurnitureCatalog LoadFromJson(string filePath)
        {
            var serializer = new JsonSerializer<FurnitureCatalog>();
            return serializer.LoadFromFile(filePath);
        }
        public void SaveToXml(string filePath)
        {
            var serializer = new XmlSerializer<FurnitureCatalog>();
            serializer.SaveToFile(this, filePath);
        }

        public static FurnitureCatalog LoadFromXml(string filePath)
        {
            var serializer = new XmlSerializer<FurnitureCatalog>();
            return serializer.LoadFromFile(filePath);
        }
    }
}