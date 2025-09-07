
namespace MVC_3.Serives
{
    public class ScopedSerives : IScopedSerives
    {
        public Guid Guid { get; set; }

        public ScopedSerives()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
