
namespace MVC_3.Serives
{
    public class TransientServies : ITransientServies
    {
        public Guid Guid { get; set; }

        public TransientServies()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
