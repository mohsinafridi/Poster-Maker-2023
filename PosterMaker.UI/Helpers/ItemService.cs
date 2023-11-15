using Newtonsoft.Json;
using PosterMaker.UI.Models;
using System.Text;

namespace PosterMaker.UI.Helpers
{
    public static class ItemService
    {
        public static async Task<List<Item>> GetItems()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/item-list");

            return JsonConvert.DeserializeObject<List<Item>>(response);
        }

        public static async Task<bool> CreateItem(Item item)
        {

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/create-item", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}
