using ECommerce.Models;

namespace ECommerce.BL.Contracts
{
        public interface IItem
        {
            public List<TbItem> GetAll();
            // this method for ViewItem
            public IQueryable<VwItem> GetAllItemsData(int? id);
            public List<VwItem> GetRecommendedItems(int itemId);
            public TbItem GetById(int id);
            // this method for item details
            public VwItem GetItemId(int id);
            public bool Save(TbItem item);
            public bool Delete(int id);
        }
    }

