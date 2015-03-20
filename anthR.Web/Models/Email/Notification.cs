using anthR.Web.Controllers;
using anthR.Web.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace anthR.Web.Models.Email
{
    public class Notification
    {

        private anthRContext db = new anthRContext();

        public void SendTaskEmail(int taskId, int staffId)
        {
            
            // load up the task
            var anthRTask = db.AnthRTask.Where(t => t.Id.Equals(taskId)).FirstOrDefault();
            StaffOnTasksController theController = CreateController<StaffOnTasksController>();
            var body = anthR.Utils.IO.Razor.RenderViewToString(theController, "EmailTaskInformation", anthRTask);

            // get the staff member details
            var staffMember = db.Staff.Where(s => s.Id.Equals(staffId)).FirstOrDefault();

            var staffEmail = staffMember.Email;
            
            Task.Factory.StartNew(() =>
            {
                
                MailMessage mailMessage = null;
                SmtpClient smtpClient = null;

                try
                {
                
                    mailMessage = new MailMessage();
                    mailMessage.To.Add(staffEmail);                
                    mailMessage.From = new MailAddress("online@kingfisher-systems.co.uk");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;
                    mailMessage.Subject = string.Format("anthR Task Assignment");

                    smtpClient = new SmtpClient();
                    smtpClient.Send(mailMessage);

                }
                catch (SmtpException smtpEx)
                {
                    throw smtpEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }).ConfigureAwait(false);
            
        }

        public static T CreateController<T>(RouteData routeData = null)
            where T : Controller, new()
        {
            T controller = new T();

            // Create an MVC Controller Context
            var wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name
                                                            .ToLower()
                                                            .Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }

    }
}
