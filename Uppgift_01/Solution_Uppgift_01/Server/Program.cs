using Server.Models;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

// Create listener
var listener = new TcpListener(IPAddress.Any, 12345);
listener.Start();
Console.WriteLine($"Server started on port 12345 | Date: {DateTime.Now.ToShortDateString()} | Time: {DateTime.Now.ToShortTimeString()}");

while (true)
{
    var client = listener.AcceptTcpClient();
    var reader = new StreamReader(client.GetStream());
    var writer = new StreamWriter(client.GetStream());

    var json = reader.ReadLine();
    var message = JsonSerializer.Deserialize<Message>(json);

    Console.WriteLine($"Received Message: {message!.Content}");

    message.Content = $" '{message.Content}' was received!";
    writer.WriteLine(JsonSerializer.Serialize(message));
    writer.Flush();

    client.Close();
}