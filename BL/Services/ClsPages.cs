using BL.Contracts;

using ECommerce.Data;
using ECommerce.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ClsPages : IPage
    {
        private readonly ILogger<ClsPages> _logger;
        private readonly AppDbContext _context;

        public ClsPages(ILogger<ClsPages> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<TbPage> GetAll()
        {
            try
            {
                var lstCategories = _context.TbPages.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public TbPage GetById(int id)
        {
            try
            {
                var itemType = _context.TbPages.FirstOrDefault(a => a.PageId == id && a.CurrentState == 1);
                return itemType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbPage itemType)
        {
            try
            {
                if (itemType.PageId == 0)
                {
                    itemType.CreatedBy = "1";
                    itemType.CreatedDate = DateTime.Now;
                    _context.TbPages.Add(itemType);
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

        public bool Dekete(int id)
        {
            try
            {
                var itemType = GetById(id);
                itemType.CurrentState = 0;
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
