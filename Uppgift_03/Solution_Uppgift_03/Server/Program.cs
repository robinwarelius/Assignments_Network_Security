using Server.Models; // Importera namnrymden för serverns modeller
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // En prefix för att lyssna på anslutningar
        listener.Start();
        Console.WriteLine("Väntar på anslutningar...");

        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync(); // Vänta på en HTTP-begäran
            if (context.Request.IsWebSocketRequest) // Kontrollera om begäran är en WebSocket-begäran
            {
                ProcessWebSocketRequest(context); // Hantera WebSocket-begäran
            }
            else
            {
                context.Response.StatusCode = 400; // Om det inte är en WebSocket-begäran, statuskod 400 (Bad Request)
                context.Response.Close();
            }
        }
    }

    static async void ProcessWebSocketRequest(HttpListenerContext context)
    {
        HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null); // Acceptera WebSocket-anslutningen

        WebSocket webSocket = webSocketContext.WebSocket;

        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                string messageToSend = "Servermeddelande: " + DateTime.Now.ToString("HH:mm:ss"); // Skapa ett meddelande att skicka till klienten

                Message message = new Message() { Content = messageToSend }; // Skapa ett Message-objekt med meddelandet

                string jsonMessage = JsonSerializer.Serialize(message); // Konvertera meddelandet till JSON

                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage); // Konvertera JSON till byte-array
                await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None); // Skicka meddelandet till klienten som byte-array
                await Task.Delay(1000); // Vänta en sekund innan nästa meddelande skickas
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message}"); // Hantera eventuella fel
        }
        finally
        {
            if (webSocket.State == WebSocketState.Open)
                webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Stängd av servern", CancellationToken.None).Wait(); // Stäng WebSocket-anslutningen när den inte längre används
        }
    }
}
