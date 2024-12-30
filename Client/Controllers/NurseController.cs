using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Client.Controllers
{
    public class NurseController : Controller
    {
        private readonly HttpClient _httpClient;
        Uri endPoint = new Uri("https://localhost:7023/api/Nus");
        public NurseController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(2);
        }
        public async Task <IActionResult> Index()
        {
            List<Nu> nus = new List<Nu>();
            try
            {
                var resp = await _httpClient.GetAsync(endPoint);

                if (resp.IsSuccessStatusCode)
                {
                    nus = await resp.Content.ReadFromJsonAsync<List<Nu>>() ?? new List<Nu>();
                    if (nus == null || !nus.Any())
                    {
                        ViewBag.eror = "لايوجد بيانات ";
                    }
                   
                }
                else
                {
                    ViewBag.eror = ".حدث خطاء اثناء جلب البيانات . ";

                }
            }
            catch (Exception ) 
            {
                        ViewBag.eror = ". خطاء غير متوقع ";

            }
            return View(nus);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
       public async Task<IActionResult> Create(Nu nu)
        {
            var rep= await _httpClient.PostAsJsonAsync<Nu>(endPoint, nu);
            if (rep.IsSuccessStatusCode)
            {
                var data = await rep.Content.ReadAsStringAsync();
                nu = JsonConvert.DeserializeObject<Nu>(data);

                if (nu != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(nu);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var respones = await _httpClient.GetFromJsonAsync<Nu>(endPoint + "/" + id);
           return View(respones);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id , Nu nu)
        {
            var respones = await _httpClient.PutAsJsonAsync<Nu>(endPoint + "/" + id,nu);

            if (respones.IsSuccessStatusCode)
            { 
            var data =await respones.Content.ReadAsStringAsync();
                nu =JsonConvert.DeserializeObject<Nu>(data);
                if (nu == null)
                {

                    return RedirectToAction(nameof(Index));

                }

            }
            return View(respones);

           
        }
        public async Task<IActionResult> Delete(int id)
        {
            var respones = await _httpClient.DeleteAsync(endPoint + "/" + id);
            return View(respones);  
        }
        public async Task <IActionResult> Delete1(int id)
        {

            var respones = await _httpClient.DeleteAsync(endPoint + "/" + id);


            if (respones.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            
            }
            return View(respones);

        }
    }

    
}
