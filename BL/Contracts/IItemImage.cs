using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface IItemImage
    {
        public List<TbItemImage> GetByItemId(int id);
    }

}
