using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicsApi.Dto
{
    public class SelectDto
    {
        public string title { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public int key { get; set; }
        public bool isChecked { get; set; }
        public bool IsHave { get; set; }
        public bool disabled { get; set; }
        public List<SelectDto> children { get; set; }

    }
}
