using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class RatingViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int? PhotoID { get; set; }
    }
}