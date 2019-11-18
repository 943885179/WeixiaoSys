//using Newtonsoft.Json;
using BasicsApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BasicsApi.Dto
{
    public  class MenuDto
    {
        public int pi { get; set; }//当前页码
        public int ps { get; set; }//每页数量，当设置为 0 表示不分页，默认：10

        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Text { get; set; }
        public string I18n { get; set; }
        public string Icon { get; set; }
        public int? PageId { get; set; }
        public int? Pagepageid { get; set; }
        public string ExternalLink { get; set; }
        public string Target { get; set; }
        public string Badge { get; set; }
        public string BadgeDot { get; set; }
        public string BadgeStatus { get; set; }
        public bool? Disabled { get; set; }
        public bool? Hide { get; set; }
        public string HideInBreadcrumb { get; set; }
        public string Acl { get; set; }
        public string Shortcut { get; set; }
        public string ShortcutRoot { get; set; }
        public string Link { get; set; }
        public string Reuse { get; set; }

        public  List<MenuDto> Children { get; set; }
    }
}
