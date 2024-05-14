using System.Net.WebSockets;
using System.Text;
using TaskT = System.Threading.Tasks.Task;


namespace RedmineServer.WebSocket
{
    public abstract class WebSocketHandler
    {
        protected ConnectionManager WebSocketConnectionManager { get; set; }

        public WebSocketHandler(ConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        public virtual async TaskT OnConnected(System.Net.WebSockets.WebSocket socket)
        {//Amikor egy kliens csatlakozik a szerverhez visszaküldünk egy üzenetet("Hello World")
            WebSocketConnectionManager.AddSocket(socket);
            //await SendMessageAsync(socket, "Hello World");
        }

        public virtual async TaskT OnDisconnected(System.Net.WebSockets.WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }

        public async TaskT SendMessageAsync(System.Net.WebSockets.WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.UTF8.GetBytes(message),
                                                                    offset: 0,
                                                                    count: Encoding.UTF8.GetBytes(message).Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true,
                                    cancellationToken: CancellationToken.None);
        }

        public async TaskT SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async TaskT SendMessageToAllAsync(string message)
        {
            foreach (var pair in WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value, message);
            }
        }

        public abstract TaskT ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
