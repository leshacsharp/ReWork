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

            TagBuilder container = new TagBuilder("ul");
            container.AddCssClass("pagination");

            TagBuilder first = CreateNavigation("<<<", url(1));
            container.InnerHtml += first.ToString();

            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("page-item");

                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", url(i));
                tag.InnerHtml = i.ToString();


                if(i == pageInfo.CurrentPage)
                    li.AddCssClass("active");       

                li.InnerHtml += tag.ToString();
                container.InnerHtml += li.ToString();
            }

            TagBuilder last = CreateNavigation(">>>", url(pageInfo.TotalPages));     
            container.InnerHtml += last.ToString();

            return MvcHtmlString.Create(container.ToString());
        }


        private static TagBuilder CreateNavigation(string title, string navigateUrl)
        {
            TagBuilder navigateLi = new TagBuilder("li");
            navigateLi.AddCssClass("page-item");

            TagBuilder navigateA = new TagBuilder("a");
            navigateA.MergeAttribute("href", navigateUrl);
            navigateA.AddCssClass("page-link");

            TagBuilder navigateSpan = new TagBuilder("span");
            navigateSpan.InnerHtml += title;
            navigateA.InnerHtml += navigateSpan.ToString();

            navigateLi.InnerHtml += navigateA.ToString();

            return navigateLi;
        }
    }
}