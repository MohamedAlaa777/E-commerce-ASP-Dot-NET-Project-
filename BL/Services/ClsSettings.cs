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
    public class ClsSettings : ISetting
    {
        private readonly ILogger<ClsSettings> _logger;
        private readonly AppDbContext _context;

        public ClsSettings(ILogger<ClsSettings> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public TbSetting GetAll()
        {
            try
            {
                var lstCategories = _context.TbSettings.FirstOrDefault();
                return lstCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbSetting setting)
        {
            try
            {
                _context.Update(setting);
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

