using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class PageElement
    {
        public int Id { get; set; }
        public int? PagId { get; set; }
        public int PageId { get; set; }
        public int ElementId { get; set; }

        public virtual Element IdNavigation { get; set; }
        public virtual Page Pag { get; set; }
    }
}
