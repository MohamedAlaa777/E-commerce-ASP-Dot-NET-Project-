using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface IPage
    {
        public List<TbPage> GetAll();
        public TbPage GetById(int id);
        public bool Save(TbPage itemType);
        public bool Dekete(int id);
    }
}
