using Model.Core;
using Model.Data.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model.Data
{
    public static class FurnitureDataGenerator
    {
        public static List<Furniture> GenerateSampleFurniture()
        {
            var furnitureList = new List<Furniture>();
            //генерация Stool
            for (int i = 1; i <= 3; i++)
            {
                furnitureList.Add(new Stool
                {
                    ArticleNumber = $"CH_00{i}",
                    Brand = "ChairBrand",
                    Model = $"Model-C{i}",
                    Description = $"Comfortable chair model C{i}",
                    BasePrice = 500 + i * 100,
                    HasArmrests = i % 2 == 0,
                    Material = i % 3 == 0 ? "Wood" : i % 3 == 1 ? "Metal" : "Plastic",
                    HasBackrest = i == 1 ? false : true,
                    IsBarStool = i == 1,
                    Height = 50 - i

                });
            }
            for (int i = 4; i <= 5; i++)
            {
                furnitureList.Add(new Armchair
                {
                    ArticleNumber = $"CH_00{i}",
                    Brand = "ChairBrand",
                    Model = $"Model-C{i}",
                    Description = $"Comfortable chair model C{i}",
                    BasePrice = 500 + i * 100,
                    HasArmrests = i % 2 == 0,
                    Material = i % 3 == 0 ? "Wood" : i % 3 == 1 ? "Metal" : "Plastic",
                    IsReclining = i == 4 ? true : false,
                    HasMassage = i == 4 ? true : false,
                });
            }
            // Генерация столов
            for (int i = 1; i <= 4; i++)
            {
                furnitureList.Add(new Table
                {
                    ArticleNumber = $"TB_00{i}",
                    Brand = "TableBrand",
                    Model = $"Model-T{i}",
                    Description = $"Durable table model T{i}",
                    BasePrice = 1000 + i * 200,
                    Shape = i % 3 == 0 ? "Round" : i % 3 == 1 ? "Square" : "Rectangle",
                    NumberOfLegs = 4
                });
            }

            // Генерация диванов
            for (int i = 1; i <= 4; i++)
            {
                furnitureList.Add(new Sofa
                {
                    ArticleNumber = $"SF_00{i}",
                    Brand = "SofaBrand",
                    Model = $"Model-S{i}",
                    Description = $"Luxurious sofa model S{i}",
                    BasePrice = 7000 + i * 500,
                    NumberOfSeats = Math.Abs(i % 2 - 4),
                    IsModular = i % 2 == 0
                });
            }

            // Генерация кроватей
            for (int i = 1; i <= 5; i++)
            {
                furnitureList.Add(new Bed
                {
                    ArticleNumber = $"BD_00{i}",
                    Brand = "BedBrand",
                    Model = $"Model-B{i}",
                    Description = $"Comfortable bed model B{i}",
                    BasePrice = 20000 + i * 400,
                    Size = i % 3 == 0 ? "Single" : i % 3 == 1 ? "Double" : "King",
                    HasStorage = i % 2 == 0
                });
            }

            return furnitureList;
        }

        public static List<FurnitureCatalog> GenerateSampleCatalogs(List<Furniture> allFurniture)
        {
            var catalogs = new List<FurnitureCatalog>();

            // Каталог "Коллекция мебели для отдыха"
            var relaxCatalog = new FurnitureCatalog
            {
                Name = "Коллекция мебели для работы"
            };
            var random = new Random();
            relaxCatalog.Add(allFurniture
                .Where(f => f is Chair || f is Table)  // Берем только стулья и столы
                .OrderBy(x => random.Next())          // Случайно перемешиваем
                .Take(8)                              // Берем первые 8
                .ToArray());
            catalogs.Add(relaxCatalog);

            // Каталог "Коллекция мебели для отдыха"
            var workCatalog = new FurnitureCatalog
            {
                Name = "Коллекция мебели для отдыха"
            };
            var random2 = new Random();
            workCatalog.Add(allFurniture
                .Where(f => f is Sofa || f is Bed)  // Берем только стулья и столы
                .OrderBy(x => random2.Next())          // Случайно перемешиваем
                .Take(8)                              // Берем первые 8
                .ToArray());
            catalogs.Add(workCatalog);

            // Каталог "Все коллекции"
            var allCatalog = new FurnitureCatalog
            {
                Name = "Выбор нашего магазина"
            };
            var random3 = new Random();
            allCatalog.Add(allFurniture
                .OrderBy(x => random3.Next())          // Случайно перемешиваем
                .Take(12)                              // Берем первые 8
                .ToArray());
            catalogs.Add(allCatalog);

            return catalogs;
        }

        public static void SaveInitialData(string basePath)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var allFurniture = GenerateSampleFurniture();
            var catalogs = GenerateSampleCatalogs(allFurniture);
            var jsonSerializer = new JsonSerializer<List<Furniture>>();
            string furnitureJsonPath = Path.Combine(basePath, "furniture.json");
            jsonSerializer.SaveToFile(allFurniture, furnitureJsonPath);

            // Сохраняем в XML
            var xmlSerializer = new XmlSerializer<List<Furniture>>();
            string furnitureXmlPath = Path.Combine(basePath, "furniture.xml");
            xmlSerializer.SaveToFile(allFurniture, furnitureXmlPath);

            // Сохраняем каталоги
            for (int i = 0; i < catalogs.Count; i++)
            {
                string catalogJsonPath = Path.Combine(basePath, $"catalog_{i}.json");
                catalogs[i].SaveToJson(catalogJsonPath);

                string catalogXmlPath = Path.Combine(basePath, $"catalog_{i}.xml");
                catalogs[i].SaveToXml(catalogXmlPath);
            }
        }
    }
}
