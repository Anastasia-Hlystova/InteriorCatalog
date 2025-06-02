using System;

namespace Model.Core
{
    public interface IFurnitureCatalog
    {
        void Add(Furniture item);
        void Add(Furniture[] items);
        bool Remove(string articleNumber);
        int Remove(Type furnitureType);
    }
}