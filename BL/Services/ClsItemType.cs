
using ECommerce.BL.Contracts;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.Extensions.Logging;

namespace ECommerce.BL.Services
{
    public class ClsItemType : IItemType
    {
        private readonly ILogger<ClsItemType> _logger;
        private readonly AppDbContext _context;
        public ClsItemType(AppDbContext context,ILogger<ClsItemType> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<TbItemType> GetAll()
        {
            try
            {
                var lst = _context.TbItemTypes.Where(a => a.CurrentState == 1).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public TbItemType GetById(int id)
        {
            try
            {
                var itemType = _context.TbItemTypes.FirstOrDefault(a => a.ItemTypeId == id && a.CurrentState == 1);
                return itemType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbItemType itemType)
        {
            try
            {
                if (itemType.ItemTypeId == 0)
                {
                    itemType.CreatedBy = "1";
                    itemType.CreatedDate = DateTime.Now;
                    _context.TbItemTypes.Add(itemType);
                }
                else
                {
                    itemType.UpdatedBy = "1";
                    itemType.UpdatedDate = DateTime.Now;
                    _context.Entry(itemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public bool Delete(int id)
        {
            try
            {
                var itemType = GetById(id);
                itemType.CurrentState = 0;
                _context.Entry(itemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
