using asw.Model;
using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly HttpClient _httpClient;
        public MedicalRecordController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(2);
        }
        Uri GetUri = new Uri("https://localhost:7023/api/MedicalRecords");
        public async Task<IActionResult> Index()
        {

            List<MedicalRecord> medicalRecords = new List<MedicalRecord>();
            try
            {
                var resp = await _httpClient.GetAsync(GetUri);


                if (resp.IsSuccessStatusCode)
                {
                    medicalRecords = await resp.Content.ReadFromJsonAsync<List<MedicalRecord>>() ?? new List<MedicalRecord>();
                    if (medicalRecords == null || !medicalRecords.Any())
                    {
                        ViewBag.erorr = "لايوجد بيانات ";
                    }
                    
                }
                else
                {
                    ViewBag.erorr = "خطاء في جلب البيانات ";


                }


            }
            catch (Exception ) 
            {
            
                        ViewBag.erorr = "خطاء غير متوقع   ";

            }

            return View(medicalRecords);


        }
   
       public async  Task<IActionResult> Create()
        {

            var doctor = await GetDoctor();
            var listDoctor = doctor.Select(d=>new SelectListItem

            {
                Value = d.ID.ToString(),
                Text = d.FullName,
            }).ToList();
            ViewBag.doctors = new SelectList(listDoctor, "Value", "Text");


            var Patient = await GetDPatient();
            var listPatient = Patient.Select(p => new SelectListItem

            {
                Value = p.ID.ToString(),
                Text = p.FullName,
            }).ToList();
            ViewBag.patient = new SelectList(listPatient, "Value", "Text");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MedicalRecord medicalRecord)
        {

            var file =  HttpContext.Request.Form.Files;
            if (file.Count()>0)
            {
                var imgName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "imgs", imgName), FileMode.Create);
                file[0].CopyTo(fileStream);
                medicalRecord.Imag_ray = imgName;
            }
           
            var resp = await _httpClient.PostAsJsonAsync<MedicalRecord>(GetUri,medicalRecord);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<MedicalRecord>();
                return RedirectToAction(nameof(Index));
            }
            else if (resp.StatusCode== System.Net.HttpStatusCode.NotFound)
            {
                return View(medicalRecord);
            }
            return View(medicalRecord);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync(GetUri + "/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<MedicalRecord>();

                return View(data);

            }
            return View("error");
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, MedicalRecord medicalRecord)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string imgName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "imgs", imgName), FileMode.Create);
                file[0].CopyTo(fileStream);
                medicalRecord.Imag_ray = imgName;



            }
            var res = await _httpClient.PutAsJsonAsync<MedicalRecord>(GetUri + "/" + id, medicalRecord);
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


        public async Task<List<Doctor>> GetDoctor()
        {
            var resp = await _httpClient.GetStringAsync("https://localhost:7023/api/Doctors");
            return JsonConvert.DeserializeObject<List<Doctor>>(resp);
        }
        public async Task<List<Patient>> GetDPatient()
        {
            var resp = await _httpClient.GetStringAsync("https://localhost:7023/api/Patients");
            return JsonConvert.DeserializeObject<List<Patient>>(resp);
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
