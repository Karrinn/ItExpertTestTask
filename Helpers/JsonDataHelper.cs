using ItExpertTestTask.Model.DTO;

namespace ItExpertTestTask.Helpers
{
    public static class JsonDataHelper
    {
        public static IList<ItemDTO> GetOrderedData(IEnumerable<Dictionary<string, string>> jsonData)
        {
            var items = jsonData.Select(kvp =>
                new ItemDTO
                {
                    Code = int.Parse(kvp.Keys.First()),
                    Value = kvp.Values.First()
                })
                .OrderBy(ob => ob.Code);

            return items;
        }
    }
}
