
using ECommerce.BL.Contracts;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.BL.Services
{
    public class ClsItem : IItem
    {
        private readonly ILogger<ClsItem> _logger;
        private readonly AppDbContext _context;

        public ClsItem(ILogger<ClsItem> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<TbItem> GetAll()
        {
            try
            {
                var lst = _context.TbItems.Where(a=>a.CurrentState==1).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll() threw an exception in ClsItem");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public IQueryable<VwItem> GetAllItemsData(int? categoryId)
        {
            try
            {
                                           //For search by category name (by id)   
                var lst = _context.VwItems.Where(a=>(a.CategoryId==categoryId || categoryId==null || categoryId==0)&&a.CurrentState==1)
                    /*.OrderByDescending(a=>a.CreatedDate)*/.AsQueryable();
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public List<VwItem> GetRecommendedItems(int itemId)
        {
            try
            {
                var item = GetById(itemId);
                var lst = _context.VwItems.Where(a => a.SalesPrice > item.SalesPrice - 150
                && a.SalesPrice < item.SalesPrice + 150
                && a.CurrentState == 1)/*.OrderByDescending(a => a.CreatedDate)*/.ToList();
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public TbItem GetById(int id)
        {
            try
            {
                var item = _context.TbItems.FirstOrDefault(c => c.ItemId == id && c.CurrentState==1);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public VwItem GetItemId(int id)
        {
            try
            {
                var item = _context.VwItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbItem item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                    item.CurrentState = 1;
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                    _context.TbItems.Add(item);
                }
                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                    _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        //Virtual delete (logical delete)
        public bool Delete(int id)
        {
            try
            {
                var item = GetById(id);
                item.CurrentState = 0;
                _context.Update(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }
    }
}
