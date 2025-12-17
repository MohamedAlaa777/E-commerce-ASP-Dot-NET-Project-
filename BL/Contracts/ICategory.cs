using ECommerce.Models;

namespace ECommerce.BL.Contracts
{
    public interface ICategory
    {
        public List<TbCategory> GetAll();
        public TbCategory GetById(int id);
        public bool Save (TbCategory category);
        public bool Delete (int id);
    }
}
