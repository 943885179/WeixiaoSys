using System;
using System.Collections.Generic;

namespace BasicsApi.Models
{
    public partial class ElementType
    {
        public ElementType()
        {
            Element = new HashSet<Element>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Element> Element { get; set; }
    }
}
