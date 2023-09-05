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
        private readonly IItemService _itemSaveService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemSaveService, ILogger<ItemController> logger)
        {
            _itemSaveService = itemSaveService;
            _logger = logger;
        }

        [HttpPost("data")]
        public async Task<IActionResult> SaveDataAsync([FromBody] IEnumerable<Dictionary<string, string>> jsonData)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(jsonData));

            var items = JsonDataHelper.GetOrderedData(jsonData).ToList();
            await _itemSaveService.SaveAsync(items);

            return Ok();
        }

        [HttpGet("data")]
        public async Task<IActionResult> GetDataAsync([FromQuery] ItemFiltrationModel filter)
        {
            var result = await _itemSaveService.GetListAsync(filter);
            var total = result.Count;

            return Ok(new { total, result });
        }

        [HttpGet("Exception")]
        public IActionResult GetException()
        {
            _logger.LogInformation("This is test exception!");
            throw new Exception("This is test exception!");
        }
    }
}