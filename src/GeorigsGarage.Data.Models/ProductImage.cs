namespace GeorigsGarage.Data.Models
{
    public class ProductImage
    {
        public string Id { get; set; }

        public string ImgUrl { get; set; }
        public string PublicId { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}