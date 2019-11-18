using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class ElementElement
    {
        public ElementElement()
        {
            PageElements = new List<PageElements>();
        }

        public int Id { get; set; }
        public int PelementId { get; set; }
        public int CelementId { get; set; }

        public virtual Element Celement { get; set; }
        public virtual Element Pelement { get; set; }
        public virtual List<PageElements> PageElements { get; set; }
    }
}
