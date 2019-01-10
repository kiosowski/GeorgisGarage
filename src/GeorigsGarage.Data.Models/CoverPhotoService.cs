namespace GeorigsGarage.Data.Models
{
    public class CoverPhotoService
    {
        public string Id { get; set; }

        public string ImageId { get; set; }
        public virtual Image Image { get; set; }

        public string ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}