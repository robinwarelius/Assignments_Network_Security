using System.Net.Sockets;
using System.Text.Json;
using Client.Models;


while (true)
{
    using var client = new TcpClient("localhost", 12345);   // Skapa en TCP-klient och anslut till servern på localhost (min egen dator) och port 12345
    using var reader = new StreamReader(client.GetStream());   // Skapa en läsare för att läsa inkommande data från servern
    using var writer = new StreamWriter(client.GetStream());   // Skapa en skrivare för att skicka data till servern

    Console.WriteLine("Enter a message to send to the server:"); 

    var input = Console.ReadLine() ?? "No message";   
    var message = new Message { Content = input };   // Skapa ett Message-objekt med användarens meddelande

    writer.WriteLine(JsonSerializer.Serialize(message));   // Skicka meddelandet till servern som JSON
    writer.Flush();   // Töm skrivbufferten

    var response = reader.ReadLine();   // Läs serverns svar
    var responseMessage = JsonSerializer.Deserialize<Message>(response);   // Deserialisera JSON-svaret till ett Message-objekt

    Console.WriteLine("Received: " + responseMessage!.Content);   // Skriv ut serverns svar på konsolen
}