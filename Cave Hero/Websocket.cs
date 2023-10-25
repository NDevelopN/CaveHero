using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
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
            var buffer = new byte[1024];
            
            _app.Map(
                "/",
                async context =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using var ws = await context.WebSockets.AcceptWebSocketAsync();
                        IOBuffer io = new IOBuffer();

                        Thread gameThread = new(() => new Cave.Game().Start(io));
                        gameThread.Start();

                        while (true)
                        {
                            Message nOutput = io.NextOutput();
                            string message = JsonSerializer.Serialize(nOutput);

                            var bytes = Encoding.UTF8.GetBytes(message);
                            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                            if (ws.State == WebSocketState.Open)
                            {
                                await ws.SendAsync(
                                    arraySegment,
                                    WebSocketMessageType.Text,
                                    true,
                                    CancellationToken.None
                                );
                            }
                            else if (
                                ws.State == WebSocketState.Closed
                                || ws.State == WebSocketState.Aborted
                            )
                            {
                                break;
                            }

                            if (nOutput.MType == MsgType.Option)
                            {
                                var reply = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                                string rec = Encoding.UTF8.GetString(buffer, 0, reply.Count);
                                Message msg = JsonSerializer.Deserialize<Message>(rec);
                                io.WriteInput(msg);
                                Thread.Sleep(500);
                            }
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
            );

            await _app.RunAsync();
        }
    }
}