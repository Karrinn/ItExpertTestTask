using ItExpertTestTask.Model;
using ItExpertTestTask.Model.DTO;

namespace ItExpertTestTask.Services.Interfaces
{
    public interface IItemSaveService
    {
        public Task<List<ItemDTO>> GetListAsync(ItemFiltrationModel filter);

        public Task SaveAsync(IEnumerable<ItemDTO> items);
    }
}
