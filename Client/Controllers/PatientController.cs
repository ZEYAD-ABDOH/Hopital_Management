using Client.Model;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.Controllers
{
    public class PatientController : Controller
    {
        
        private readonly HttpClient _httpClient;

        public PatientController(HttpClient httpClient)
        { 
             _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(2);

        }
        Uri GetUri = new Uri("https://localhost:7023/api/Patients");
        public async Task<IActionResult> Index()
        {

            List<Patient> patients = new List<Patient>();


            try
            {

                var resp = await _httpClient.GetAsync(GetUri);


                if (resp.IsSuccessStatusCode)
                {
                    patients = await resp.Content.ReadFromJsonAsync<List<Patient>>() ?? new List<Patient>();
                    if (patients == null || !patients.Any())
                    {
                        ViewBag.error = "بيانات فارغة";
                    }
                   
                }
                else
                {
                    ViewBag.error = "بيانات فارغة";

                }
            }
            catch (Exception)
            {
                ViewBag.error = "خطاء غير متوقع ";

            }



            return View(patients);



        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {



            var resp = await _httpClient.PostAsJsonAsync<Patient>(GetUri, patient);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Patient>();
                return RedirectToAction(nameof(Index));
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View(patient);
            }
            return View(patient);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync(GetUri + "/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Patient>();

                return View(data);

            }
            return View("error");
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Patient patient)
        {




            var res = await _httpClient.PutAsJsonAsync<Patient>(GetUri + "/" + id, patient);
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
