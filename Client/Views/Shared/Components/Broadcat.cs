using Client.Model;
using Microsoft.AspNetCore.Mvc;

namespace Client.Views.Shared.Components
{
    public class BroadcatViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public BroadcatViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _httpClient.GetFromJsonAsync<List<Patient>>("https://localhost:7023/api/Patients");
            var countPat = res?.Count ?? 0;
            var viewmodel =
                new
                {
                    CountPat = countPat,
                };
            return View("Default", viewmodel);
        }
    }
}
