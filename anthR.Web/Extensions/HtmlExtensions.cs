using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

    }
}
