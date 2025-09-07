namespace MVC_3.Serives
{
    public interface ISingletonSerives
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
