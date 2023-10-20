using System.Net;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Server
{
    public class Websocket
    {
        private static Websocket? _instance;
        private WebApplicationBuilder _builder;
        private WebApplication _app;

        private Websocket()
        {
            _builder = WebApplication.CreateBuilder();
            _builder.WebHost.UseUrls("http://localhost:8090");
            _app = _builder.Build();
            _app.UseWebSockets();
        }

        public static Websocket GetInstance()
        {
            if (_instance == null)
            {
                _instance = new();
            }

            return _instance;
        }


        public async Task Start()
        {
            _app.Map("/ws", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using var ws = await context.WebSockets.AcceptWebSocketAsync();
                    while (true)
                    {
                        var message = "The current time is : " + DateTime.Now.ToString("HH:mm:ss");
                        var bytes = Encoding.UTF8.GetBytes(message);
                        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                        if (ws.State == WebSocketState.Open)
                        {
                            await ws.SendAsync(arraySegment,
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None);
                        }
                        else if (ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
                        {
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            });

            await _app.RunAsync();
        }
    }
}