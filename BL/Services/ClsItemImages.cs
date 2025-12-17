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
    public class ClsItemImages : IItemImage
    {
        private readonly ILogger<ClsItemImages> _logger;
        private readonly AppDbContext _context;

        public ClsItemImages(ILogger<ClsItemImages> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<TbItemImage> GetByItemId(int id)
        {
            try
            {
                var item = _context.TbItemImages.Where(a => a.ItemId == id).ToList();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }
    }
}


