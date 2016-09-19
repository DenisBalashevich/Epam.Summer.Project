using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class PhotoViewModel
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TagViewModel> Tags { get; set; }
        public ICollection<RatingViewModel> Ratings { get; set; }
    }
}