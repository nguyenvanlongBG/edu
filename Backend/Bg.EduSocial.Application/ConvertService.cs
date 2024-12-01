using Bg.EduSocial.Constract;
using System.Text.Json;

namespace Bg.EduSocial.Application
{
    public class ConvertService : IConvertService
    {
        public T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        public string Serialize(object value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
