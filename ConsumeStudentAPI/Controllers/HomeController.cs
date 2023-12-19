using ConsumeStudentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ConsumeStudentAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string Url = "https://localhost:7080/";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public  IActionResult Index()
        {
            return View();
        }
        /*public IActionResult Privacy()
        {
            var Token = Request.Cookies["token"];
            if (string.IsNullOrEmpty(Token))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }*/

        public  IActionResult PrivacyAsync()
        {
            var jwtToken = Request.Cookies["user"];
            if (string.IsNullOrEmpty(jwtToken)) 
            { 
                return RedirectToAction("Index", "Home");
            }
           /* using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Student");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var dt = JsonConvert.DeserializeObject<List<Student>>(result);
                    ViewData.Model = dt;
                }
                else
                {
                    Console.WriteLine("Error Calling get api");
                }
            }*/
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}