namespace HardCode.WebAPI.Models
{
    public class CategoryModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public class CategoryRequestModel
    {
        public string? Name { get; set; }
    }
}
