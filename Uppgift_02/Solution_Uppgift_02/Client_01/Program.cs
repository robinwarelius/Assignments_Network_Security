using System.Net.Sockets;
using System.Net;
using System.Text;


int port = 12345;
UdpClient udpClient = new UdpClient();
udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));

Console.WriteLine("Waiting on messages...");

// Starta tråd för att lyssna på inkommande meddelanden
Thread receiveThread = new Thread(() =>
{
    while (true)
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] data = udpClient.Receive(ref remoteEndPoint);
        string message = Encoding.UTF8.GetString(data);
        Console.WriteLine($"Recieved message from {remoteEndPoint}: {message}");
    }
});
receiveThread.Start();

