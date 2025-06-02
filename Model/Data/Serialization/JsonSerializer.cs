using Model.Core.Serialization;
using Newtonsoft.Json;
using System;

namespace Model.Data.Serialization
{
    public class JsonSerializer<T> : BaseSerializer<T>
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializer(JsonSerializerSettings settings = null)
        {
            _settings = settings ?? new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
        }

        public override string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        public override T Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, _settings);
        }
    }
}