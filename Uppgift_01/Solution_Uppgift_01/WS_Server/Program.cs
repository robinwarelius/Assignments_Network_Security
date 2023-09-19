using System.Net;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using WS_Server.Methods;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.UseRouting();

var wsOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(120) };
app.UseWebSockets(wsOptions);



app.Run();


