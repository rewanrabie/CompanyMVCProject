namespace MVC_3.Serives
{
    public interface IScopedSerives
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
