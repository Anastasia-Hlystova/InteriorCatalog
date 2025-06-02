using System;
using System.IO;

namespace Model.Core.Serialization
{
    public abstract class BaseSerializer<T>
    {
        public abstract string Serialize(T obj);
        public abstract T Deserialize(string data);

        public void SaveToFile(T obj, string filePath)
        {
            string serializedData = Serialize(obj);
            File.WriteAllText(filePath, serializedData);
        }

        public T LoadFromFile(string filePath)
        {
            string data = File.ReadAllText(filePath);
            return Deserialize(data);
        }
    }
}