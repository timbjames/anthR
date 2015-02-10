using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

    }
}
