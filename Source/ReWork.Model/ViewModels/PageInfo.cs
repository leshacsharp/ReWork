using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Model.ViewModels
{
    public class PageInfo
    {
        public int CurrentPage { get; set; }

        public int ItemsOnPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages {
            get
            {
                return TotalItems / ItemsOnPage;
            }
        }
    }
}
