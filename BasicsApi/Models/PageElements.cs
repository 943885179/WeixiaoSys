using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class PageElements
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int ElementsId { get; set; }

        public virtual ElementElement Elements { get; set; }
        public virtual Page Page { get; set; }
    }
}
