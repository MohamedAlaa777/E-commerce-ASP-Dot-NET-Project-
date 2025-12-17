using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface ISlider
    {
        public List<TbSlider> GetAll();
        public TbSlider GetById(int id);
        public bool Save(TbSlider os);
        public bool Dekete(int id);
    }
}
