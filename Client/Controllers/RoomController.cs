using Client.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Cryptography.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.Controllers
{
    public class RoomController : Controller
    {
        private readonly HttpClient _httpClient ;
        public RoomController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(2);
        }
        Uri GetUri = new Uri("https://localhost:7023/api/Rooms");
        public async Task<IActionResult> Index()
        {

            List<Room> data = new List<Room>();
            try
            {
                var resp = await _httpClient.GetAsync(GetUri);


                if (resp.IsSuccessStatusCode)
                {
                    data = await resp.Content.ReadFromJsonAsync<List<Room>>() ?? new List<Room>();
                    if (data == null || !data.Any())
                    {
                       ViewBag.errer = ".لايوجد بيانات " ;
                    }

                }
                else
                {

                   ViewBag.errer = ".حدث خطاء اثناء جلب البيانات ." ;
                }

                
            }
            catch (Exception ) 
            {
               ViewBag.errer = ".حدث خطاء عير متوقع " ;
            }

            return View(data);


        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {



            var resp = await _httpClient.PostAsJsonAsync<Room>(GetUri, room);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Room>();
                return RedirectToAction(nameof(Index));
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View(room);
            }
            return View(room);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync(GetUri + "/" + id);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<Room>();

                return View(data);

            }
            return View("error");
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int id, Room room)
        {


            var res = await _httpClient.PutAsJsonAsync<Room>(GetUri + "/" + id, room);
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
