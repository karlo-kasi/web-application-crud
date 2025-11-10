namespace WebApplication_scuffolding_reverse.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Color { get; set; }
        public decimal? ListPrice { get; set; }
        public string? ThumbnailPhotoFileName { get; set; }
    }
}
