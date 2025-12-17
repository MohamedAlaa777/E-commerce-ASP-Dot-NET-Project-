using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface ISalesInvoiceItem
    {
        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id);

        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew);
    }
}
