using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface ISetting
    {
        public TbSetting GetAll();
        public bool Save(TbSetting setting);
    }
}
