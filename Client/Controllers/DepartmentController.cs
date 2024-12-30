
using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class DepartmentController : Controller
    {
         
      private readonly HttpClient _httpClient;
       
        Uri GetEndPoint = new Uri(" http://www.managhospitalym.somee.com/api/Departls");
        public DepartmentController(HttpClient httpClient)
        {
            _httpClient = httpClient ;
            _httpClient.Timeout = TimeSpan.FromSeconds(2);
        }
        public async Task<IActionResult> Index()
        {
            //var resp= await httpClient.GetFromJsonAsync<List<Departl>>(GetEndPoint);

            //return View(resp);  

            List<Departl> departls = new List<Departl>();

            var resp = await _httpClient.GetAsync(GetEndPoint);

            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadAsStringAsync();
                departls = JsonConvert.DeserializeObject<List<Departl>>(data);
                return View
                    (departls);
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View("غير موجودة");
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.BadGateway)
            {
                return View("خطاء من سرفر ");
            }
            else
            {
                return View("not Found");
            }
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Departl departl)
        {
            var resp = await _httpClient.PostAsJsonAsync<Departl>(GetEndPoint, departl);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadAsStringAsync();
                departl = JsonConvert.DeserializeObject<Departl>(data);

                if (departl != null)
                {

                    return RedirectToAction(nameof(Index));


                }
            }
            return View(departl);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetFromJsonAsync<Departl>(GetEndPoint + "/" + id);
            return View(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Departl departl)
        {
            var resp = await _httpClient.PutAsJsonAsync<Departl>(GetEndPoint + "/" + id, departl);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadAsStringAsync();
                departl = JsonConvert.DeserializeObject<Departl>(data);
                if (departl == null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departl);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var respones = await _httpClient.DeleteAsync(GetEndPoint + "/" + id);
            return View(respones);
        }
        public async Task<IActionResult> Delete1(int id)
        {

            var respones = await _httpClient.DeleteAsync(GetEndPoint + "/" + id);


            if (respones.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));

            }
            return View(respones);

        }
    }
}

