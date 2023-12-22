using ItemStore.Dtos;
using ItemStore.Entities;
using ItemStore.Interfaces;
using ItemStore.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ItemStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        //private readonly IItemService _itemService;
        private readonly ItemServiceEF _itemService;

        //public ItemController(IItemService itemService)
        //{
        //    _itemService = itemService;
        //}
        public ItemController(ItemServiceEF itemService)
        {
            _itemService = itemService;
        }
     
        //[HttpGet("{id}/buy")]
        //public async Task<IActionResult> BuyItem(int id, int quantity)
        //{
        //    try
        //    {
        //        decimal totalPrice = await _itemService.BuyItem(id, quantity);

        //        return Ok(totalPrice);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
      
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _itemService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _itemService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm]ItemDto itemDto)
        {
            await _itemService.Create(itemDto);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Edit(ItemDto itemDto)
        {
            await _itemService.Edit(itemDto);
            return Ok("Item updated successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.Delete(id);
            //string response = "Item deleted";
            //dynamic json = JsonConvert.DeserializeObject(response);
            return Ok("Item deleted");
        }
    }
}
