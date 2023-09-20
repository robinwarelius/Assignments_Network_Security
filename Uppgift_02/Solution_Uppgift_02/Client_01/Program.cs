using System.Net.Sockets;
using System.Net;
using System.Text;
using Client_01.Models;
using System.Text.Json;

int port = 12345; // Ange portnumret för kommunikation

UdpClient udpClient = new UdpClient(); // Skapa en UDP-klient för att hantera kommunikation

// Bind UDP-klienten till vilken lokal IP-adress och port som helst
udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port)); // Här gör vi bindning till alla tillgängliga nätverksgränssnitt

Console.WriteLine("Waiting on messages..."); 

Thread receiveThread = new Thread(() => // Skapa en tråd för att hantera mottagning av meddelanden
{
    while (true)
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] data = udpClient.Receive(ref remoteEndPoint); // Ta emot meddelande och ange avsändarens IP-adress och port
        string jsonMessage = Encoding.UTF8.GetString(data); // Konvertera inkommande bytes till en textsträng

        Message? message = JsonSerializer.Deserialize<Message>(jsonMessage); // Deserialisera JSON till ett Message-objekt

        Console.WriteLine($"Received message from {remoteEndPoint}: {message.Content}"); // Skriv ut meddelandets avsändare och innehåll
    }
});
receiveThread.Start(); // Starta tråden för att lyssna på inkommande meddelanden

