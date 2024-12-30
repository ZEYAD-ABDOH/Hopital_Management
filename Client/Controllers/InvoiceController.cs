using Client.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Client.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly HttpClient _httpClient;
        public InvoiceController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }
        Uri GetUri = new Uri("https://localhost:7023/api/Invoices");
        public async Task<IActionResult> Index()
        {
            var resp = await _httpClient.GetAsync(GetUri);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<List<Invoice>>();
                return View(data);
            }

            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("Not fiuund");
            }

            return View("server error");


        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice )
        {

          

            var resp = await _httpClient.PostAsJsonAsync<Invoice>(GetUri, invoice);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Invoice>();
                return RedirectToAction(nameof(Index));
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View(invoice);
            }
            return View(invoice);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync(GetUri + "/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Invoice>();

                return View(data);

            }
            return View("error");
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Invoice invoice)
        {
            


            
            var res = await _httpClient.PutAsJsonAsync<Invoice>(GetUri + "/" + id, invoice);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }
            if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("In server");
            }

            return View("ther is error ");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var respones = await _httpClient.DeleteAsync(GetUri + "/" + id);
            return View(respones);
        }
        public async Task<IActionResult> Delete1(int id)
        {

            var respones = await _httpClient.DeleteAsync(GetUri + "/" + id);


            if (respones.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }
            return View(respones);

        }
    }
}
