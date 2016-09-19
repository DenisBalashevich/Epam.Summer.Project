using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class UserProfileModel
    {
        public UserViewModel UserInfo { get; set; }
        public PhotoViewModel Photo { get; set; }
    }
}