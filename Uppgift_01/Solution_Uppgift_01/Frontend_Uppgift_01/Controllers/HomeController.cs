
using Frontend_Uppgift_01.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace Frontend_Uppgift_01.Controllers
{
    public class HomeController : Controller
    {           
        public HomeController()
        {
            
        }

        public async Task<IActionResult> Index()
        {
            string serverIp = "127.0.0.1";
            int serverPort = 7255;

          
            using var client = new TcpClient();
              
            client.Connect(serverIp, serverPort);

            var message = "hello";
             
            using NetworkStream netWorkStream = client.GetStream();

            using var reader = new StreamReader(netWorkStream, Encoding.UTF8);

            byte[] bytes = Encoding.UTF8.GetBytes(reader.ReadToEnd());








            netWorkStream.Close();
            client.Close();
            
            
            return View();
        }

    }
}








//public async Task <IActionResult> Index()
//{
//    HttpClient client = _clientFactory.CreateClient("");

//    HttpRequestMessage message = new HttpRequestMessage();

//    message.Method = HttpMethod.Get;

//    message.RequestUri = new Uri("https://localhost:7255/api/Home/ReturnSometing");

//    HttpResponseMessage? apiResponse = await client.SendAsync(message);

//    var apiContent = await apiResponse.Content.ReadAsStringAsync();

//    var listData = JsonConvert.DeserializeObject<List<Data>>(apiContent);

//    return View();
//}