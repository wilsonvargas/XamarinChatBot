using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Clients.Services.BotService
{
    public interface IBotService
    {
        Task Connect();
        Task SendMessage();
    }
}
