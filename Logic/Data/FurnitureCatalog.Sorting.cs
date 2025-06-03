using Model.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Model.Data
{
    public partial class FurnitureCatalog : ISortable
    {
        public void Sort(bool ascending = false)
        {
            Items = ascending
                ? Items.OrderBy(i => i.ArticleNumber).ToList()
                : Items.OrderByDescending(i => i.ArticleNumber).ToList();
        }

        public void SortByName(bool ascending = false)
        {
            Items = ascending
                ? Items.OrderBy(i => i.Brand).ThenBy(i => i.Model).ToList()
                : Items.OrderByDescending(i => i.Brand).ThenByDescending(i => i.Model).ToList();
        }

        public void SortByPrice(bool ascending = false)
        {
            Items = ascending
                ? Items.OrderBy(i => i.Price).ToList()
                : Items.OrderByDescending(i => i.Price).ToList();
        }
    }
}