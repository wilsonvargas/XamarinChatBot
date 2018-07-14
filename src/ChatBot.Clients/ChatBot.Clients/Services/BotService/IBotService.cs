using ChatBot.Clients.Models;
using System.Threading.Tasks;

namespace ChatBot.Clients.Services.BotService
{
    public interface IBotService
    {
        Task<Activity> Connect();

        Task<Activity> SendMessage(Activity message);
    }
}
