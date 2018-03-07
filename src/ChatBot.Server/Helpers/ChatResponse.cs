using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBot.Server.Helpers
{
    public static class ChatResponse
    {
        public static readonly string Greeting = "I can answer your questions about Xamarin Restaurant. \n\n" +
           "For reservations queries, please contact the restaurant directly on +51 999999999.";

        public static readonly string Delivery =
            "We have delivery from 2 PM to 12 AM. You can call 32656566 to start the delivery";

        public static readonly string Wifi = "Yes, we have complimentary wifi for clients in all area.";

        public static readonly string Location =
            "We are located on the city centre, a five minute walk to the Museum and surrounded by shops, bars, restaurants and other tourist attractions.";

        public static readonly string Farewell = "Thanks for chatting. Goodbye.";

        public static readonly string Default = "Sorry I didn't understand. Can you say that again please?";

        public static readonly string Parking = "We have a carpark for all clients. It is free to use during your stay.";

        public static readonly string Thanks = "Glad I could help.";
    }
}