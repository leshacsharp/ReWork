using ReWork.Model.ViewModels;
using System;
using System.Text;
using System.Web.Mvc;

namespace ReWork.WebSite.Helpers
{
    public static class Paging
    {
        public static MvcHtmlString CreatePaging(this HtmlHelper htmlHelper, PageInfo pageInfo, Func<int, string> url)
        {
            StringBuilder html = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", url(i));
                tag.InnerHtml = i.ToString();


                if(i == pageInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn btn-primary");
                }
                else
                {
                    tag.AddCssClass("btn btn-default");
                }

                html.Append(tag.ToString());
            }

            return MvcHtmlString.Create(html.ToString());
        }
    }
}