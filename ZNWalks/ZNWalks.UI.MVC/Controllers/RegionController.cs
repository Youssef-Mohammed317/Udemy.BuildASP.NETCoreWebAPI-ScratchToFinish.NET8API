using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NZWalks.UI.MVC.CustomFilters;
using NZWalks.UI.MVC.Models.RegionDTOs;

namespace NZWalks.UI.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = new List<RegionViewModel>();

            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync("https://localhost:7106/api/v1/region");

            httpResponseMessage.EnsureSuccessStatusCode();

            var responseBody = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionViewModel>>();

            response.AddRange(responseBody!);

            return View(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync($"https://localhost:7106/api/v1/region/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionViewModel>();

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromForm] CreateRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7106/api/v1/region"),
                Content = new StringContent(JsonSerializer.Serialize(model),
                Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionViewModel>();
            return RedirectToAction("Details", "Region", new { id = response?.Id });
        }

        [HttpGet("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync($"https://localhost:7106/api/v1/region/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionViewModel>();

            return View(response);
        }

        [HttpPost("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7106/api/v1/region/{id}"),
                Content = new StringContent(JsonSerializer.Serialize(model),
                Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionViewModel>();

            return RedirectToAction("Details", "Region", new { id = response?.Id });

        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync($"https://localhost:7106/api/v1/region/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionViewModel>();

            return View(response);
        }
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> DeleteAction(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.DeleteAsync($"https://localhost:7106/api/v1/region/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = httpResponseMessage.Content.ReadFromJsonAsync<RegionViewModel>();

            return RedirectToAction("Index", "Region");
        }
    }
}
