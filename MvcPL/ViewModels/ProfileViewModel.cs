using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class ProfileViewModel
    {
        public UserInformationViewModel UserInformation { get; set; }
        public IndexViewModel<PhotoViewModel> AllUserPhotos { get; set; }
        public string UserName { get; set; }
    }
}