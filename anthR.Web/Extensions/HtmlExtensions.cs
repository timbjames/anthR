using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace anthR.Web
{
    public static class HtmlExtension
    {

        public static IHtmlString DisplayFormattedFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression)
        {

            var value = Convert.ToString(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);

            if (string.IsNullOrEmpty(value))
            {
                return MvcHtmlString.Empty;
            }

            value = string.Join("<br />", value.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(HttpUtility.HtmlEncode));

            return new HtmlString(value);

        }

        /// <summary>
        /// Allows RAW html to be placed in the default Html.ActionLink
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="rawHtml"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString RawActionLink(this HtmlHelper htmlHelper, string rawHtml, string action, string controller, object routeValues, object htmlAttributes){

            string holder = Guid.NewGuid().ToString();
            string anchor = htmlHelper.ActionLink(holder, action, controller, routeValues, htmlAttributes).ToString();
            return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));

        }

        /// <summary>
        /// Adds an active class to the currently active link
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static IHtmlString ActiveActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller)
        {                      
            return htmlHelper.ActiveActionLink(linkText, action, new { controller = controller });
        }

        /// <summary>
        /// Adds an active class to the currently active link
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static IHtmlString ActiveActionLink(this HtmlHelper htmlHelper, string linkText, string action, object routeValues )
        {
            
            string activeClass = string.Empty;
            string controller = string.Empty;

            // get the controller from the routeValues object
            Type type = routeValues.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());

            foreach (var prop in props)
            {
                if (prop.Name.Equals("controller"))
                {
                    controller = prop.GetValue(routeValues, null).ToString();
                }                
            }
            
            if ( htmlHelper.ViewContext.RouteData.Values["action"].ToString().Equals(action, StringComparison.OrdinalIgnoreCase)
                &&
            htmlHelper.ViewContext.RouteData.Values["controller"].ToString().Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                // set the active class for the matching link
                activeClass = "active";
            }

            //render the link with the class
            string anchor = htmlHelper.ActionLink(linkText, action, routeValues, new { @class = activeClass }).ToString();

            return MvcHtmlString.Create(anchor);

        }

    }
}
