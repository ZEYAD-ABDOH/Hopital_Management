using asw.Model;
using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class DoctorController : Controller
    {

        private readonly HttpClient _httpClient;
        public DoctorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(25);
        }

        Uri GetEndPoint = new Uri("https://localhost:7023/api/Doctors");

        public async Task<IActionResult> Index()
        {
            var resp = await _httpClient.GetFromJsonAsync<List<Doctor>>(GetEndPoint);

            var dapert = await _httpClient.GetFromJsonAsync<List<Departl>>("https://localhost:7023/api/Departls");

            ViewBag.Departl = dapert;

            return View(resp);



        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            var resp = await _httpClient.PostAsJsonAsync<Doctor>(GetEndPoint, doctor);

            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadAsStringAsync();
                doctor = JsonConvert.DeserializeObject<Doctor>(data);
                if (doctor != null)
                {
                    return RedirectToAction(nameof(Index));

                }

            }
            return View(doctor);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetFromJsonAsync<Doctor>(GetEndPoint + "/" + id);
            return View(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {

            var resp = await    _httpClient.PutAsJsonAsync<Doctor>(GetEndPoint + "/" + id, doctor);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadAsStringAsync();
                doctor = JsonConvert.DeserializeObject<Doctor>(data);

                if (doctor == null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(doctor);


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

