namespace HardCode.WebAPI.Entities
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}
