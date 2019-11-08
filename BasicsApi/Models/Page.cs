using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Page
    {
        public Page()
        {
            Menu = new HashSet<Menu>();
            PageElement = new HashSet<PageElement>();
            PageElements = new HashSet<PageElements>();
            PagePageCpage = new HashSet<PagePage>();
            PagePagePpage = new HashSet<PagePage>();
            RolePage = new HashSet<RolePage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<PageElement> PageElement { get; set; }
        public virtual ICollection<PageElements> PageElements { get; set; }
        public virtual ICollection<PagePage> PagePageCpage { get; set; }
        public virtual ICollection<PagePage> PagePagePpage { get; set; }
        public virtual ICollection<RolePage> RolePage { get; set; }
    }
}
