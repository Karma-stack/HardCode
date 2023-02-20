namespace HardCode.WebAPI.Models
{
    public class ProductRequestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public string? AdditionalFields { get; set; }
        public bool ImageChange { get; set; }
    }
}
