using Client.Model;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly HttpClient _httpClient;
        Uri GetUri = new Uri(" http://www.managhospitalym.somee.com/api/Appointments");

    
        public AppointmentController(HttpClient httpClient)
        {
            _httpClient=httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }
        public async Task<IActionResult> Index()


        {
            List<Appointment>data = new List<Appointment>();
            try
            {
                var resp = await _httpClient.GetAsync(GetUri);
                if (resp.IsSuccessStatusCode)
                {
                    data = await resp.Content.ReadFromJsonAsync<List<Appointment>>() ?? new List<Appointment>();
                    if (data == null || !data.Any())
                    {
                        ViewBag.eror = "لا يوجد بيانات ";

                    }

                  
                }
                else
                {
                    ViewBag.eror = "حدث خطاء اثناء جلب البيانات";

                }

            }
            catch (Exception ) 
            { 
            
                        ViewBag.eror = "خطاء غير متوقع ";

            }


            return View(data);


        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {



            var resp = await _httpClient.PostAsJsonAsync<Appointment>(GetUri, appointment);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Appointment>();
                return RedirectToAction(nameof(Index));
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View(appointment);
            }
            return View(appointment);
        }
          


        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync(GetUri + "/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Appointment>();

                return View(data);

            }
            return View("error");
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {




            var res = await _httpClient.PutAsJsonAsync<Appointment>(GetUri + "/" + id, appointment);
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
