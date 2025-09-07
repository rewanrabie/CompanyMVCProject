
namespace MVC_3.Serives
{
    public class SingletonSerives : ISingletonSerives
    {
        public Guid Guid { get; set; }

        public SingletonSerives()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
