using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class PagePage
    {
        public PagePage()
        {
            Menu = new List<Menu>();
        }

        public int Id { get; set; }
        public int PpageId { get; set; }
        public int CpageId { get; set; }

        public virtual Page Cpage { get; set; }
        public virtual Page Ppage { get; set; }
        public virtual List<Menu> Menu { get; set; }
    }
}
