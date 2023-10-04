using IoT_Unit.Model;
using IoT_Unit.Utilities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// Simulerat en enhet med random nummer från 1-20
while (true)
{
    Random random = new Random();
    string secretValue = $"Thermometer,Great For you're House,{random.Next(1, 20).ToString()}";
    string apiUrl = "https://localhost:7080/api/unit/CreateUnit";
    await ApiBase.SendAsync(apiUrl, secretValue);
    await Task.Delay(5000);
}



