using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bazar.Areas.Admin.Infrastructure
{
    public class SidebarMenuItem
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string FaIcon { get; set; }
        public bool Selected { get; set; }
        public List<SidebarMenuItem> Items { get; set; }
        public int? Count { get; set; }
    }
}
