using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NoName.Hubs
{
    public class NoteHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
