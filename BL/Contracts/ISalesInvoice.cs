using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface ISalesInvoice
    {
        public List<VwSalesInvoice> GetAll();

        public TbSalesInvoice GetById(int id);
        public List<VwSalesInvoice> GetbyUserId(Guid userid);

        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew);

        public bool Delete(int id);
    }
}
