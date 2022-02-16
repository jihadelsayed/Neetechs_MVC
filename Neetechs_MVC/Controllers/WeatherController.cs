using Microsoft.AspNetCore.Mvc;
using Neetechs_MVC.Model;
using Newtonsoft.Json;

namespace Neetechs_MVC.Controllers
{
    public class WeatherController : Controller
    {
        public async Task<IActionResult> WeatherTask(string town)
        {
            Console.WriteLine(town);
            if (String.IsNullOrEmpty(town))
            {
                town = HttpContext.Session.GetString("town");
            }
            if (String.IsNullOrEmpty(town))
            {
                town = "Malmö";
            }
            Weather weather = await GetWeather(town);
            ViewData["Temperature"] = weather.Current.TempC;
            ViewData["Town"] = weather.Location.Name;
            ViewData["image"] = weather.Current.Condition.Icon;
            HttpContext.Session.SetString("town", town);
            return PartialView("_Weather");
        }
        private async Task<Weather> GetWeather(string town)
        {
            HttpClient client = new HttpClient();
            Weather weather = null;
            string uri = "https://api.weatherapi.com/v1/current.json?key=df8aad9677454db9b0180343221502&q=";
            string url = uri + town;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                weather = JsonConvert.DeserializeObject<Weather>(await response.Content.ReadAsStringAsync());
            }
            return weather;
        }
    }
}
