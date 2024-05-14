using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using TaskT=System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Mvc;

namespace RedmineServer.WebSocket
{
    
    public class HelloWorldHandler : WebSocketHandler
    {

        public HelloWorldHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
            
        }
        public override async TaskT ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string email =  Encoding.UTF8.GetString(buffer, 0, result.Count) ;
            Console.WriteLine(email) ;
            HttpResponseMessage response = await new HttpClient().GetAsync($"http://localhost:5300/api/tasks/timed/?email="+email+"");
            Console.WriteLine(response.ToString()) ;
            Console.WriteLine(response.Content.ToString());
            var resultText=await  response.Content.ReadAsStringAsync() ;
            await SendMessageAsync(socket, resultText);
        }
    }
}
