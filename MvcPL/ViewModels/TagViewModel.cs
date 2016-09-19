using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PhotoViewModel> Photos { get; set; }
    }
}