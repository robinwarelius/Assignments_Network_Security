using Client_02.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

int port = 12345;
UdpClient udpClient = new UdpClient();

while (true)
{
    Console.Write("Enter a message: ");
    string messageToSend = Console.ReadLine();

    Message message = new Message() { Content = messageToSend }; // Skapa ett Message-objekt

    string jsonMessage = JsonSerializer.Serialize(message); // Konvertera Message-objektet till JSON-sträng

    byte[] data = Encoding.UTF8.GetBytes(jsonMessage); // Konvertera JSON-strängen till bytes

    udpClient.Send(data, data.Length, "127.0.0.1", port); // Skicka meddelandet via UDP till angiven IP och port
}
