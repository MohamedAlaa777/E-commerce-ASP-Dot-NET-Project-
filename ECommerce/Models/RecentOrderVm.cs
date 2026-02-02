namespace ECommerce.Models
{
    public class RecentOrderVm
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CurrentState { get; set; }
        public Guid CustomerId { get; set; }
    }
}
