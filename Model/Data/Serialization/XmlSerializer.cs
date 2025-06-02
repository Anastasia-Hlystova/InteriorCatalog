using Model.Core;
using Model.Core.Serialization;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Model.Data.Serialization
{
    public class XmlSerializer<T> : BaseSerializer<T>
    {
        private readonly XmlSerializer _serializer;
        public XmlSerializer()
        {
            var extraTypes = new[] { typeof(Chair), typeof(Table), typeof(Sofa), typeof(Bed), typeof(Stool), typeof(Armchair) };
            _serializer = new XmlSerializer(typeof(T), extraTypes);
        }
        public override string Serialize(T obj)
        {
            using (var writer = new StringWriter())
            {
                _serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public override T Deserialize(string data)
        {
            using (var reader = new StringReader(data))
            {
                return (T)_serializer.Deserialize(reader);
            }
        }
    }
}