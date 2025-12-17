namespace ECommerce.Models
{
    //this model doesn't link with database
    public class ShoppingCartItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        //total of each item
        public decimal Total { get; set; }
    }
}
