using Microsoft.Owin;
using Owin;
using anthR;

namespace anthR.Web
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }

    }
}
