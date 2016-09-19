using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcPL.ViewModels;

namespace MvcPL.Infastructure.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("input");

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                if (Math.Abs(i - pageInfo.PageNumber) < 3 || i == 1 || i == pageInfo.TotalPages)
                {
                    tag.MergeAttribute("type", "submit");
                    tag.MergeAttribute("value", i.ToString());

                    tag.MergeAttribute("name", "page");
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag);
                }
                else result.Append("..");
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}

