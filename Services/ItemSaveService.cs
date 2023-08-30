using ItExpertTestTask.Data;
using ItExpertTestTask.Data.Entities;
using Microsoft.EntityFrameworkCore;
using ItExpertTestTask.Services.Interfaces;
using ItExpertTestTask.Model.DTO;
using ItExpertTestTask.Model;

namespace ItExpertTestTask.Services
{
    public class ItemSaveService : IItemSaveService
    {
        private readonly ApplicationContext dbContext;

        public ItemSaveService(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<ItemDTO>> GetListAsync(ItemFiltrationModel filter)
        {
            var query = dbContext
                .Items
                .AsNoTracking()
                .AsQueryable();

            if (filter.Code.HasValue)
                query = query.Where(x => x.Code == filter.Code);

            if (!string.IsNullOrEmpty(filter.Value))
                query = query.Where(x => x.Value.Contains(filter.Value));

            return await query
                .Select(x => new ItemDTO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Value = x.Value
                })
                .ToListAsync();
        }

        private async Task ClearAsync()
        {
            await dbContext.Items.ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(IEnumerable<ItemDTO> items)
        {
            await ClearAsync();

            var entities = items
                .Select(s => new Item {
                    Code = s.Code,
                    Value = s.Value
                });

            await dbContext.Items.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
