using Newtonsoft.Json;
using PosterMaker.UI.DTOs;
using PosterMaker.UI.Models;
using System.Text;

namespace PosterMaker.UI.Helpers
{
    public static class AppService
    {
        public static async Task<List<App>> GetApps()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/app-list");

            return JsonConvert.DeserializeObject<List<App>>(response);
        }


        public static async Task<List<AppCategory>> GetCategoriesByAppId(int appId)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/app-categories/"+appId);

            return JsonConvert.DeserializeObject<List<AppCategory>>(response);
        }
        public static async Task<bool> CreateApp(App app)
        {

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(app);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/create-app", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}
