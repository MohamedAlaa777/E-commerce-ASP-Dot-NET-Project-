using BL.Contracts;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISetting _Settings;
        public SettingController(ISetting oISettings)
        {
            _Settings = oISettings;
        }
        // GET: api/<SettingController>
        [HttpGet]
        public TbSetting Get()
        {
            var oSeeting = _Settings.GetAll();
            return oSeeting;
        }
    }
}
