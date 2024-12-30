
using Client.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        Uri GetUri = new Uri("https://localhost:7023/api/Users");
        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();

            try
            {
                var resq = await _httpClient.GetAsync(GetUri);



                if (resq.IsSuccessStatusCode)
                {
                    users = await resq.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();
                    if (users == null || !users.Any())
                    {
                        ViewBag.errer = ".لايوجد بيانات ";
                    }
                    else
                    {
                        ViewBag.errer = ".حدث خطاء اثناء جلب البيانات .";
                    }

                }


            }
            catch (Exception)
            {
                ViewBag.errer = ".حدث خطاء عير متوقع ";

            }


            List<Departl> depart = new List<Departl>();

            using (var resq = await _httpClient.GetAsync("https://localhost:7023/Departls\r\n"))

            {

                if (resq.IsSuccessStatusCode)
                {
                    var data = await resq.Content.ReadAsStringAsync();
                    depart = JsonConvert.DeserializeObject<List<Departl>>(data);

                }
            }

            ViewBag.Departs = depart;

            return View(users);
        }


        public async Task<IActionResult> Create()
        {
            var rol = HttpContext.Session.GetString("Role");

            if( rol == "User" || rol == "Superisor")
            {
                TempData["ErroMessge"] = "لا يوجد صلاحية للا ضافة";
                return RedirectToAction(nameof(Index));
            }

           
           return View();
        }

        [ValidateAntiForgeryToken]// csrf 
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {

            user.Password = Encryptpass(user.Password);

            var resp = await _httpClient.PostAsJsonAsync<User>("https://localhost:7023/api/Users", user);
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<User>();
                return RedirectToAction(nameof(Index));
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return View(user);
            }
            return View(user);
        }


        public string Encryptpass(string password)
        {
            if (password == null)
            {
                throw new ArgumentException(nameof(password));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {

                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();

            }
        }


        [HttpPost]


        public async Task<IActionResult> Login(userlogin userlogin)
        {

            //var accessToken = await HttpContext.GetTokenAsync("");

            //if (accessToken==null)
            //{
            //    return Unauthorized("لايوجد");
            //}


            //var c = _httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer",accessToken);



            var resp = await _httpClient.PostAsJsonAsync("https://localhost:7023/api/Users/login", userlogin);

            if (resp.IsSuccessStatusCode)
            {

                TempData["erorr"] = "تم تسجيل الدخول بنجاح";
                //return RedirectToAction("Index","Doctor");
                var respa = await resp.Content.ReadFromJsonAsync<TokenResponse>();
                var token = respa.Token;
                var role= respa.Role;

                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("Auth", token);
                    HttpContext.Session.SetString("Role", role);
               
                    //return Ok(

                    //    new
                    //    {
                    //        token = token,
                    //    }
                    //    );



                }
                var userrole = HttpContext.Session.GetString("Role");

                if (userrole == "Admin" )
                {
                    return RedirectToAction("Index", "Patient");

                    //return Ok( new
                    //{

                    //    token1 = token,
                    //    rol = role
                    //}
                    //    );
                }

                else if (userrole == "User")
                {
                    return RedirectToAction("Index", "User");


                } 
                else if (userrole == "Superisor")
                {
                    return RedirectToAction("Index", "User");


                } 
                else if (userrole == "Sales")
                {
                    return RedirectToAction("Index", "Patient");


                }
                //return Ok(

                //       new
                //       {
                //           token = token,
                //       }
                //       );

            }


           
            
                ViewBag.erorr = "خطاء";
                return View(userlogin);
            

        }
    
        //هذي  بطريقة qeuer م عاد بحتاجة هذي اذ كان تشتي حقك يكون محلي
        //public async Task<IActionResult> Login(User user)
        //{
        //    if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
        //    {

        //        ViewBag.error = "يجب ادخال المستخدم وكلمة السر";

        //        return View(user);

        //    }
        //    string connt = @"Server=DESKTOP-S5MSF27\\SQLEXPRESS; Database=Hospital_Management; User ID=sa; Password=1234; Trusted_connection=True; MultipleActiveResultSets=true; Encrypt=false";

        //    using (SqlConnection connetion = new SqlConnection(connt))
        //    {
        //        connetion.Open();
        //        string query = "select count(*) from users where UserName=@UserName and Password=@Password ";
        //        SqlCommand cmd = new SqlCommand(query, connetion);
        //        cmd.Parameters.AddWithValue("@UserName", user.UserName);
        //        cmd.Parameters.AddWithValue("@Password", user.Password);

        //        int userLogin = (int)cmd.ExecuteScalar();
        //        if (userLogin > 0)
        //        {

        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ViewBag.error = "المستخدم  غير موجود حول مره اخر ";
        //            return View(user);
        //        }
        //    }
        //}
        public IActionResult Login()
        {
            return View();
        }




        public async Task<IActionResult> Edit(int id)

        {


        


            User users = new User();
            var rol = HttpContext.Session.GetString("Role");
            if (rol == "Superisor")
            {
                ViewBag.MessgeUpdate = " يمكنك التعديل فقط ";

                using (var httpclient = new HttpClient())
                {
                    using (var resq = await httpclient.GetAsync("https://localhost:7023/api/Users" + "/" + id))

                    {

                        if (resq.IsSuccessStatusCode)
                        {
                            var datatrans = await resq.Content.ReadAsStringAsync();
                            users = JsonConvert.DeserializeObject<User>(datatrans);

                        }
                    }
                }
            }
                return View(users);
            

        
            //var respones = await httpClient.GetFromJsonAsync<User>("https://localhost:7023/api/Users" + "/" + id);
            //return View(respones);

        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id , User user)
        {

                User user1 = new User();
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var resource = await _httpClient.PutAsync("https://localhost:7023/api/Users" + "/" + id, stringContent))
                {
                    var data = await resource.Content.ReadAsStringAsync();
                    user1 = JsonConvert.DeserializeObject<User>(data);


                }
                     if (user1 == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(user1);
            }
        

        public async Task<IActionResult> Delete(int id)
        {


            var rol = HttpContext.Session.GetString("Role");

            if (rol == "Superisor")
            {
                TempData["ErroMessge"] = "لا يوجد صلاحية للا حذف";
                
           

            User users = new User();

            using (var httpclient = new HttpClient())
            {
                using (var resq = await httpclient.GetAsync("https://localhost:7023/api/Users" + "/" + id))

                {

                    if (resq.IsSuccessStatusCode)
                    {
                        var datatrans = await resq.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<User>(datatrans);

                    }
                }
                }
            }
            return RedirectToAction(nameof(Index));
            //return View(users);
            //var response = await httpClient.GetFromJsonAsync<User>("https://localhost:7023/api/Users" + "/"+id);


            //return View(response);
        }

        public async Task<IActionResult> DeleteOk(int id)


        {

            var resp = await _httpClient.DeleteAsync("https://localhost:7023/api/Users" + "/" + id);
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
   
    
    
    
    
    }
}
