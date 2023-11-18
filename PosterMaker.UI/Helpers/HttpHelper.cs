using Newtonsoft.Json;
using PosterMaker.UI.Models;
using System.Text;

namespace PosterMaker.UI.Helpers;

public static class HttpHelper
{

    #region Login

    public static async Task<bool> LoginUser(LoginViewModel loginViewModel)
    {
        var httpClient = new HttpClient();
        var json = JsonConvert.SerializeObject(loginViewModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/login", content);
        if (!response.IsSuccessStatusCode) return false;
        return true;
    }
    #endregion





    #region "Item Service"

    #endregion
}
