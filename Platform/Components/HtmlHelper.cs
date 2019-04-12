using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Platform.Components
{
    public static class ControlsHelper { 

    public static MvcHtmlString CommentsBoxFor<TModel>(
      this HtmlHelper<TModel> htmlHelper,
      string text,
      string title,
      int width,
      int height) where TModel : MemberDatabase.Contact
    {

        StringBuilder sb = new StringBuilder(512);
       
            sb.Append(@"<div class=""col - md - 2"">");
            sb.Append("<h2>Comments</h2>");
       
        foreach(var comment in TModel.comments)
            {

            }

        // Add the Text
        sb.Append(text);

       
            sb.Append("</label>");
            sb.Append("</label>");
            sb.Append("</div>");
   

        return MvcHtmlString.Create(sb.ToString());
    }

    }
}