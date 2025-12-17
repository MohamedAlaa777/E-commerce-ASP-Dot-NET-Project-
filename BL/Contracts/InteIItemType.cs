using ECommerce.Models;

namespace ECommerce.BL.Contracts
{
    public interface IItemType
    {
        public List<TbItemType> GetAll();
        public TbItemType GetById(int id);
        public bool Save(TbItemType itemType);
        public bool Delete(int id);
    }
}
