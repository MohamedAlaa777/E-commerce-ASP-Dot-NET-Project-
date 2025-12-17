
using ECommerce.BL.Contracts;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.Extensions.Logging;

namespace ECommerce.BL.Services
{
    public class ClsCategory : ICategory
    {
        private readonly ILogger<ClsCategory> _logger;
        private readonly AppDbContext _context;

        public ClsCategory(ILogger<ClsCategory> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<TbCategory> GetAll()
        {
            try
            {
                var lst = _context.Set<TbCategory>().Where(a=>a.CurrentState==1).ToList();
                return lst;
            }
            //not selint catch
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll() threw an exception in ClsCategory");
                throw new Exception("Custom message for GetAll failure", ex);
            }

        }

        public TbCategory GetById(int id)
        {
            try
            {
                var item = _context.TbCategories.FirstOrDefault(c => c.CategoryId == id && c.CurrentState==1);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById() threw an exception in ClsCategory");
                throw new Exception("Custom message for GetById failure", ex);
            }
        }

        public bool Save(TbCategory category)
        {
            try
            {
                if (category.CategoryId == 0)
                {
                    category.CurrentState = 1;
                    category.CreatedBy = "1";
                    category.CreatedDate = DateTime.Now;
                    _context.TbCategories.Add(category);
                }
                else
                {
                    category.UpdatedBy = "1";
                    category.UpdatedDate = DateTime.Now;
                    _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveOrUpdate() threw an exception in ClsCategory");
                throw new Exception("Custom message for Save or update failure", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var category = GetById(id);
                category.CurrentState = 0;
                _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete() threw an exception in ClsCategory");
                throw new Exception("Custom message for Delete failure", ex);
            }
        }
    }
}
