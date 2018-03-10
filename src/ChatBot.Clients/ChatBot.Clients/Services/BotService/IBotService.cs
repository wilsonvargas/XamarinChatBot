using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Clients.Models;

namespace ChatBot.Clients.Services.BotService
{
    public interface IBotService
    {
        Task<Activity> Connect();
        Task<Activity> SendMessage(string message);
    }
}
