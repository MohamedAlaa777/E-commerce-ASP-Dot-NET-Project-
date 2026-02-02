using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ECommerce.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var startDate = DateTime.Today.AddDays(-7);

            //  Weekly Sales
            var weeklySales = _context.TbSalesInvoiceItems
                .Where(i => i.Invoice.InvoiceDate >= startDate)
                .Sum(i => (decimal?)i.InvoicePrice * (decimal)i.Qty) ?? 0;

            //  Weekly Orders
            var weeklyOrders = _context.TbSalesInvoices
                .Count(i => i.InvoiceDate >= startDate);

            //  Recent Orders
            var recentOrders = _context.TbSalesInvoices
                .OrderByDescending(i => i.InvoiceDate)
                .Take(5)
                .Select(i => new RecentOrderVm
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceDate = i.InvoiceDate,
                    CurrentState = i.CurrentState,
                    CustomerId = i.CustomerId
                })
                .ToList();

            //  Monthly Sales (Chart)
            var monthlySales = _context.TbSalesInvoiceItems
                .Where(i => i.Invoice.InvoiceDate.Year == DateTime.Now.Year)
                .GroupBy(i => i.Invoice.InvoiceDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(x => x.InvoicePrice * (decimal)x.Qty)
                })
                .OrderBy(x => x.Month)
                .ToList();



            //  Best Selling Items (Chart)
            var bestItems = _context.TbSalesInvoiceItems
                .GroupBy(i => i.Item.ItemName)
                .Select(g => new
                {
                    ItemName = g.Key,
                    Qty = g.Sum(x => x.Qty)
                })
                .OrderByDescending(x => x.Qty)
                .Take(5)
                .ToList();

            var vm = new AdminDashboardVm
            {
                WeeklySales = weeklySales,
                WeeklyOrders = weeklyOrders,
                RecentOrders = recentOrders,

                MonthlySalesLabels = monthlySales
                    .Select(x => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(x.Month))
                    .ToList(),

                MonthlySalesValues = monthlySales
                    .Select(x => x.Total)
                    .ToList(),

                BestItemsLabels = bestItems
                    .Select(x => x.ItemName)
                    .ToList(),

                BestItemsQty = bestItems
                    .Select(x => x.Qty)
                    .ToList()
            };

            return View(vm);
        }
    }
}
