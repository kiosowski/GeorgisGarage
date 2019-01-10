namespace GeorigsGarage.Data.Models
{
    public class CartProduct
    {
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }
    }
}