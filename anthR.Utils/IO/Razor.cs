using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace anthR.Utils.IO
{
     public class Razor
    {

        /// <summary>
        /// Reads a MVC View into a string object
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderViewToString(Controller controller, string viewName, object model)
        {
            // set the controller view data model based on our model passed in
            controller.ViewData.Model = model;

            // create our return paramter
            string result = string.Empty;

            try
            {
                using (StringWriter sw = new StringWriter())
                {

                    ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    result = sw.ToString();

                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle error */
            }

            return result;

        }

        /// <summary>
        /// Reads a MVC View into a string object
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            // set the controller view data model based on our model passed in
            controller.ViewData.Model = model;

            // create our return paramter
            string result = string.Empty;

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    result = sw.ToString();

                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle error */
            }

            return result;

        }

        /// <summary>
        /// Creates a Controller outside of a ControllerContext
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routeData"></param>
        /// <returns></returns>
        public static T CreateController<T>(RouteData routeData = null)
            where T : Controller, new()
        {

            // create a disconnected controller instance
            T controller = new T();

            // get the context wrapper from HttpContext if available
            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
            {
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            }
            else
            {
                throw new InvalidOperationException("Can't create Controller Context if no active HttpContext instance is available");
            }

            if (routeData == null)
            {
                routeData = new RouteData();
            }

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
            {
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));
            }

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);

            return controller;

        }

    }
}
