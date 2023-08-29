using Microsoft.AspNetCore.Mvc;
using ItExpertTestTask.Services.Interfaces;
using ItExpertTestTask.Model;
using ItExpertTestTask.Helpers;

namespace ItExpertTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemSaveService _itemSaveService;

        public ItemController(IItemSaveService itemSaveService)
        {
            _itemSaveService = itemSaveService;
        }

        [HttpPost("data")]
        public async Task<IActionResult> SaveDataAsync([FromBody] IEnumerable<Dictionary<string, string>> jsonData)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(jsonData));

            var items = JsonDataHelper.GetOrderedData(jsonData);
            await _itemSaveService.SaveAsync(items);

            return Ok();
        }

        [HttpGet("data")]
        public async Task<IActionResult> GetDataAsync([FromQuery] int? code, [FromQuery] string? value)
        {
            var filter = new ItemFiltrationModel
            {
                Code = code,
                Value = value
            };

            var result = await _itemSaveService.GetListAsync(filter);
            var total = result.Count;

            return Ok(new { total, result });
        }

    }
}