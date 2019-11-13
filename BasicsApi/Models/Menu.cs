//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BasicsApi.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Children = new List<Menu>();
            RoleMenu = new List<RoleMenu>();
        }

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
        public Boolean? Disabled { get; set; }
        public Boolean? Hide { get; set; }
        public string HideInBreadcrumb { get; set; }
        public string Acl { get; set; }
        public string Shortcut { get; set; }
        public string ShortcutRoot { get; set; }
        public string Link { get; set; }
        public string Reuse { get; set; }

       // [JsonIgnore]
        public virtual Menu P { get; set; }
        public virtual Page Page { get; set; }
        public virtual PagePage Pagepage { get; set; }
       //[JsonIgnore]
        public virtual List<Menu> Children { get; set; }
        public virtual List<RoleMenu> RoleMenu { get; set; }
    }
}
