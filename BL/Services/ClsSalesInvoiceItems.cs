using BL.Contracts;

using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BL.Services
{
    public class ClsSalesInvoiceItems : ISalesInvoiceItem
    {
        private readonly ILogger<ClsSalesInvoiceItems> _logger;
        private readonly AppDbContext _context;

        public ClsSalesInvoiceItems(ILogger<ClsSalesInvoiceItems> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id)
        {
            try
            {
                var Items = _context.TbSalesInvoiceItems.Where(a => a.InvoiceId == id).ToList();
                if (Items == null)
                    return new List<TbSalesInvoiceItem>();
                else
                    return Items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }
                         //interface Items
        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew)
        {
            List<TbSalesInvoiceItem> dbInvoiceItems =
                GetSalesInvoiceId(Items[0].InvoiceId);

            foreach (var interfaceItems in Items)
            {
                var dbObject = dbInvoiceItems.Where(a => a.InvoiceItemId == interfaceItems.InvoiceItemId).FirstOrDefault();
                if (dbObject != null)
                {
                    _context.Entry(dbObject).State = EntityState.Modified;
                }
                else
                {
                    //all items saved to salesinvoice where all have the same InvoiceId
                    interfaceItems.InvoiceId = salesInvoiceId;
                    _context.TbSalesInvoiceItems.Add(interfaceItems);
                }
            }

            foreach (var item in dbInvoiceItems)
            {
                var interfaceObject = Items.Where(a => a.InvoiceItemId == item.InvoiceItemId).FirstOrDefault();
                if (interfaceObject == null)
                    _context.TbSalesInvoiceItems.Remove(item);
            }

            _context.SaveChanges();
            return true;
        }
    }
}

