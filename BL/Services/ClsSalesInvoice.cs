using BL.Contracts;

using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ClsSalesInvoice : ISalesInvoice
    {
        private readonly ILogger<ClsSalesInvoice> _logger;
        private readonly AppDbContext _context;
        private readonly ISalesInvoiceItem _salesInvoiceItemsService;

        public ClsSalesInvoice(ILogger<ClsSalesInvoice> logger, AppDbContext context, ISalesInvoiceItem salesInvoiceItemsService)
        {
            _logger = logger;
            _context = context;
            _salesInvoiceItemsService = salesInvoiceItemsService;
        }

        public List<VwSalesInvoice> GetAll()
        {
            try
            {
                return _context.VwSalesInvoices.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public List<VwSalesInvoice> GetbyUserId(Guid userid)
        {
            try
            {
                return _context.VwSalesInvoices.Where(a=>a.CustomerId==userid).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public TbSalesInvoice GetById(int id)
        {
            try
            {
                var Item = _context.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (Item == null)
                    return new TbSalesInvoice();
                else
                    return Item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Item.CurrentState = 1;
                if (isNew)
                {
                    Item.CreatedBy = "1";
                    Item.CreatedDate = DateTime.Now;
                    _context.TbSalesInvoices.Add(Item);
                }

                else
                {
                    Item.UpdatedBy = "1";
                    Item.UpdatedDate = DateTime.Now;
                    _context.Entry(Item).State = EntityState.Modified;
                }

                _context.SaveChanges();
                _salesInvoiceItemsService.Save(lstItems, Item.InvoiceId, true);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        //Actual delete
        public bool Delete(int id)
        {
            try
            {
                var Item = _context.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (Item != null)
                {
                    _context.TbSalesInvoices.Remove(Item);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }
    }
}
