using ECommerce.Models;

namespace ECommerce.BL.Contracts
{
    public interface IOs
    {
        public List<TbO> GetAll();
        public TbO GetById(int id);
        public bool Save(TbO os);
        public bool Delete(int id);
    }
}
