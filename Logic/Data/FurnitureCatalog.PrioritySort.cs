using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data
{
    public partial class FurnitureCatalog
    {
        public void PrioritySort()
        {
            Items = Items.OrderBy(i => i.Brand, StringComparer.OrdinalIgnoreCase)
                         .ThenBy(i => i.Model, StringComparer.OrdinalIgnoreCase)
                         .ThenBy(i => i.Description, StringComparer.OrdinalIgnoreCase)
                         .ThenBy(i => i.ArticleNumber, StringComparer.OrdinalIgnoreCase)
                         .ToList();
        }
    }
}