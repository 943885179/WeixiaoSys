using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Element
    {
        public Element()
        {
            ElementElementCelement = new List<ElementElement>();
            ElementElementPelement = new List<ElementElement>();
            RoleElement = new List<RoleElement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public int? OperationId { get; set; }

        public virtual Operation Operation { get; set; }
        public virtual ElementType TypeNavigation { get; set; }
        public virtual PageElement PageElement { get; set; }
        public virtual List<ElementElement> ElementElementCelement { get; set; }
        public virtual List<ElementElement> ElementElementPelement { get; set; }
        public virtual List<RoleElement> RoleElement { get; set; }
    }
}
