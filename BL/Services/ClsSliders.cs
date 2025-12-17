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
    public class ClsSliders : ISlider
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClsSliders> _logger;

        public ClsSliders(AppDbContext context, ILogger<ClsSliders> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TbSlider> GetAll()
        {
            try
            {
                var lstCategories = _context.TbSliders.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public TbSlider GetById(int id)
        {
            try
            {
                var os = _context.TbSliders.FirstOrDefault(a => a.SliderId == id && a.CurrentState == 1);
                return os;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbSlider os)
        {
            try
            {
                if (os.SliderId == 0)
                {
                    os.CreatedBy = "1";
                    os.CreatedDate = DateTime.Now;
                    _context.TbSliders.Add(os);
                }
                else
                {
                    os.UpdatedBy = "1";
                    os.UpdatedDate = DateTime.Now;
                    _context.Entry(os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var os = GetById(id);
                os.CurrentState = 0;
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
