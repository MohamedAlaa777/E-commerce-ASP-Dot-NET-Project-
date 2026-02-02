namespace ECommerce.Models
{
    public class AdminDashboardVm
    {
        // ===== Top Cards =====
        public decimal WeeklySales { get; set; }
        public int WeeklyOrders { get; set; }

        // ===== Tables =====
        public List<RecentOrderVm> RecentOrders { get; set; } = new();

        // ===== Charts =====
        public List<string> MonthlySalesLabels { get; set; } = new();
        public List<decimal> MonthlySalesValues { get; set; } = new();

        public List<string> BestItemsLabels { get; set; } = new();
        public List<double> BestItemsQty { get; set; } = new();
    }
}
