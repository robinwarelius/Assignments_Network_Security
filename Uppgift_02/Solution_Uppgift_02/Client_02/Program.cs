using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 12345;
UdpClient udpClient = new UdpClient();

// Send message
while (true)
{
    Console.Write("Enter a message: ");
    string messageToSend = Console.ReadLine();
    byte[] data = Encoding.UTF8.GetBytes(messageToSend);
    udpClient.Send(data, data.Length, "127.0.0.1", port);
}


