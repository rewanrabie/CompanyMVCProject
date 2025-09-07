namespace MVC_3.Serives
{
    public interface ITransientServies
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
