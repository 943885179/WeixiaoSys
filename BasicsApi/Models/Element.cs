using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class Element
    {
        public Element()
        {
            ElementElementCelement = new HashSet<ElementElement>();
            ElementElementPelement = new HashSet<ElementElement>();
            RoleElement = new HashSet<RoleElement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public int? OperationId { get; set; }

        public virtual Operation Operation { get; set; }
        public virtual ElementType TypeNavigation { get; set; }
        public virtual PageElement PageElement { get; set; }
        public virtual ICollection<ElementElement> ElementElementCelement { get; set; }
        public virtual ICollection<ElementElement> ElementElementPelement { get; set; }
        public virtual ICollection<RoleElement> RoleElement { get; set; }
    }
}
