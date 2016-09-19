using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class PhotoInformViewModel
    {
        public int RatingsCount { get; set; }
        public int PhotoId { get; set; }
        public bool IsSelected { get; set; }
    }
}