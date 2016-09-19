using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class EditUserViewModel
    {
        public UserViewModel User { get; set; }
        public IEnumerable<RoleViewModel> AllRoles { get; set; }
    }
}