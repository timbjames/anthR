using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace anthR.Hub
{
        
    public class NotificationHub : Microsoft.AspNet.SignalR.Hub
    {

        public void Message(Message message)
        {
            Clients.All.message(message);
        }

    }

    public class Message{
        public string msg { get; set; }
        public string user { get; set; }
    }

}
