using Client.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        using (ClientWebSocket clientWebSocket = new ClientWebSocket())
        {
            Uri serverUri = new Uri("ws://localhost:8080"); // Skapa en URI för WebSocket-servern
            await clientWebSocket.ConnectAsync(serverUri, CancellationToken.None); // Anslut till servern med WebSocket

            while (clientWebSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]); // Skapa en buffert för att ta emot meddelanden
                WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(buffer, CancellationToken.None); // Ta emot data från servern
                string jsonMessage = Encoding.UTF8.GetString(buffer.Array, 0, result.Count); // Konvertera mottaget byte-array till en JSON-sträng

                Message? message = JsonSerializer.Deserialize<Message>(jsonMessage); // Deserialisera JSON till en Message-objekt

                Console.WriteLine($"Mottaget meddelande från servern: {message.Content}"); // Skriv ut meddelandets innehåll
            }
        }
    }
}
