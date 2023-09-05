using ItExpertTestTask.Model;
using ItExpertTestTask.Model.DTO;

namespace ItExpertTestTask.Services.Interfaces
{
    public interface IItemService
    {
        Task<IList<ItemDTO>> GetListAsync(ItemFiltrationModel filter);

        Task SaveAsync(IEnumerable<ItemDTO> items);
    }
}
