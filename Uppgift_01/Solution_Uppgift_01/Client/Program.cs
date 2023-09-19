using System.Net.Sockets;
using System.Text.Json;
using Client.Models;


while (true)
{
    using var client = new TcpClient("localhost", 12345);
    using var reader = new StreamReader(client.GetStream());
    using var writer = new StreamWriter(client.GetStream());

    Console.WriteLine("Enter a message to send to the server:");
    var input = Console.ReadLine() ?? "No message";
    var message = new Message { Content = input };
    writer.WriteLine(JsonSerializer.Serialize(message));
    writer.Flush();

    var response = reader.ReadLine();
    var responseMessage = JsonSerializer.Deserialize<Message>(response);

    Console.WriteLine("Received: " + responseMessage!.Content);
}