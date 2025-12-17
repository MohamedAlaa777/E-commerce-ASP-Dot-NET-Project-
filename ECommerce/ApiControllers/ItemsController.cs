using ECommerce.ApiModels;
using ECommerce.BL.Contracts;
using ECommerce.BL.Services;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItem _item;

        public ItemsController(IItem item)
        {
            _item = item;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _item.GetAll();
            return Ok(result);
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _item.GetById(id);
            if (result == null) return NotFound($"No item is found with ID: {id}");
            var dto = new ItemDto
            {
                ItemName = result.ItemName,
                ItemTypeId = result.ItemTypeId,
                CategoryId = result.CategoryId,
                ImageName = result.ImageName,
                OsId = result.OsId,
                Description = result.Description,
                Gpu = result.Gpu,
                HardDisk = result.HardDisk,
                Processor = result.Processor,
                RamSize = result.RamSize,
                ScreenReslution = result.ScreenReslution,
                ScreenSize = result.ScreenSize,
                Weight = result.Weight,
            };
            return Ok(dto); 
        }

        // POST api/<ItemsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ItemDto item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TbItem tbItem = new TbItem 
            {
                ItemName=item.ItemName,
                ItemTypeId=item.ItemTypeId,
                CategoryId=item.CategoryId,
                ImageName=item.ImageName,
                OsId=item.OsId,
                Description=item.Description,
                Gpu=item.Gpu,
                HardDisk=item.HardDisk,
                Processor=item.Processor,
                RamSize=item.RamSize,
                ScreenReslution=item.ScreenReslution,
                ScreenSize=item.ScreenSize,
                Weight=item.Weight,
                SalesPrice=item.SalesPrice,
                PurchasePrice=item.PurchasePrice,
            };
            _item.Save(tbItem);
            return Ok(tbItem);
        }

        // PUT api/<ItemsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var result = _item.GetById(id);
            if (result == null) return NotFound($"No item is found with ID: {id}");
            _item.Delete(id);
            return Ok(result);
        }
    }
}
