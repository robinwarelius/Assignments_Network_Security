using System.Net.WebSockets;
using System.Net;
using WS_Server.Methods;

namespace WS_Server.StartupExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            

            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.Use(async (context, next) =>
            {
                if (context.Request.Path == "/send")
                {
                    using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                    {
                        await Method.Send(context, webSocket);
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            });




            return app;
        }

        

    }
}
