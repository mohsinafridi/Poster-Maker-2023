using Newtonsoft.Json;
using PosterMaker.UI.Models;
using System.Text;

namespace PosterMaker.UI.Helpers
{
    public static class CategoryService
    {
        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/category-list");

            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<bool> CreateCategory(Category category)
        {

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/create-category", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}
