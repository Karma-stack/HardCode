namespace HardCode.WebAPI.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int ImageId { get; set; }
        public Image? Image { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? AdditionalFields { get; set; }
    }
}
