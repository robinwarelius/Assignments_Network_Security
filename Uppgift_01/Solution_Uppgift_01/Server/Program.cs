using Server.Models;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

// Skapa en lyssnare (listener) för att lyssna på inkommande anslutningar
var listener = new TcpListener(IPAddress.Any, 12345); // Lyssnar på alla tillgängliga nätverksgränssnitt på port 12345
listener.Start(); // Starta lyssnaren
Console.WriteLine($"Server started on port 12345 | Date: {DateTime.Now.ToShortDateString()} | Time: {DateTime.Now.ToShortTimeString()}");

while (true)
{
    var client = listener.AcceptTcpClient(); // Acceptera en inkommande TCP-anslutning från en klient
    var reader = new StreamReader(client.GetStream()); // Skapa en läsare för att läsa inkommande data från klienten
    var writer = new StreamWriter(client.GetStream()); // Skapa en skrivare för att skicka data till klienten

    var json = reader.ReadLine(); // Läs en rad med JSON-data från klienten
    var message = JsonSerializer.Deserialize<Message>(json); // Deserialisera JSON-data till ett Message-objekt

    Console.WriteLine($"Received Message: {message!.Content}");

    message.Content = $" '{message.Content}' was received!";
    writer.WriteLine(JsonSerializer.Serialize(message)); // Skicka det ändrade meddelande tillbaka till klienten som JSON
    writer.Flush(); // Töm skrivbufferten

    client.Close(); // Stäng TCP-anslutningen med klienten
}