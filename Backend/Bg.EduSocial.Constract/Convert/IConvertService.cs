namespace Bg.EduSocial.Constract
{
    public interface IConvertService
    {
        string Serialize(object value);
        T Deserialize<T>(string value);
    }
}
