using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Page
    {
        public Page()
        {
            Menu = new List<Menu>();
            PageElement = new List<PageElement>();
            PageElements = new List<PageElements>();
            PagePageCpage = new List<PagePage>();
            PagePagePpage = new List<PagePage>();
            RolePage = new List<RolePage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual List<Menu> Menu { get; set; }
        public virtual List<PageElement> PageElement { get; set; }
        public virtual List<PageElements> PageElements { get; set; }
        public virtual List<PagePage> PagePageCpage { get; set; }
        public virtual List<PagePage> PagePagePpage { get; set; }
        public virtual List<RolePage> RolePage { get; set; }
    }
}
