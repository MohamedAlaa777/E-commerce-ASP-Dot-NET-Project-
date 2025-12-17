
using ECommerce.BL.Contracts;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.Extensions.Logging;

namespace ECommerce.BL.Services
{
    public class ClsOs : IOs
    {
        private readonly ILogger<ClsOs> _logger;
        private readonly AppDbContext _context;
        public ClsOs(AppDbContext context, ILogger<ClsOs> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<TbO> GetAll()
        {
            try
            {
                var lstOs = _context.TbOs.Where(a => a.CurrentState == 1).ToList();
                return lstOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public TbO GetById(int id)
        {
            try
            {
                var os = _context.TbOs.FirstOrDefault(a => a.OsId == id && a.CurrentState == 1);
                return os;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "methodName() threw an exception in ClassName");
                throw new Exception("Custom message for methodName failure", ex);
            }
        }

        public bool Save(TbO os)
        {
            try
            {
                if (os.OsId == 0)
                {
                    os.CreatedBy = "1";
                    os.CreatedDate = DateTime.Now;
                    _context.TbOs.Add(os);
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

        public bool Delete(int id)
        {
            try
            {
                var os = GetById(id);
                os.CurrentState = 0;
                _context.Entry(os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
