using IoT_Unit.Model;
using IoT_Unit.Model.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Unit.Utilities
{
    public static class ApiBase
    {
        // Skickar ett krypterat objekt via http till mitt backend system
        public static async Task SendAsync(string apiUrl, string secretValue)
        {
            EncryptedDto encryptedDto = Encryption.EncryptData(secretValue);
            using (HttpClient httpClient = new HttpClient())
            {            
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiUrl);
                message.Method = HttpMethod.Post;
                message.Content = new StringContent(JsonConvert.SerializeObject(encryptedDto), Encoding.UTF8, "application/json");
                await httpClient.SendAsync(message);
            }
        }
    }
}
