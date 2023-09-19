using System.Net.WebSockets;
using System.Text;

namespace WS_Server.Methods
{
    public static class Method
    {
        public static async Task Send (HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);

            if(result != null)
            {
                while(!result.CloseStatus.HasValue)
                {
                    string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));
                    Console.WriteLine($"Client says: {msg}");
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes($"Server says {DateTime.UtcNow:f}")), result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
                    //Console.WriteLine(result);
                }
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, System.Threading.CancellationToken.None);
        }
    }
}
