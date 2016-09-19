using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Interfaces;
using BLL.Services;
using MvcPL.Infastructure.Mappers;
using MvcPL.ViewModels;

namespace MvcPL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserInformationService _userInformationService;
        public UserController(IUserService userService, IUserInformationService userInformationService)
        {
            if (ReferenceEquals(userService, null))
                throw new ArgumentNullException(nameof(userService));
            if (ReferenceEquals(userInformationService, null))
                throw new ArgumentNullException(nameof(userService));

            _userInformationService = userInformationService;
            _userService = userService;
        }

        /// <summary>
        /// Shows users
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1)
        {
            List<UserViewModel> users = null;
            try
            {
                users = _userService.GetAll().Select(user => user.ToMVCUser()).ToList();
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            foreach (var u in users)
            {
                try
                {
                    u.Profile = _userInformationService.GetByUserId(u.Id).ToMVCInformationUsers();
                }
                catch
                {
                    return RedirectToAction("Error", "Error");
                }
            }
            int pageSize = 10;

            var ivm = new IndexViewModel<UserViewModel>(page, pageSize, users);
            if (Request.IsAjaxRequest())
                return PartialView(ivm);
            return View(ivm);
        }


        /// <summary>
        /// Delete users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            UserViewModel user = null;
            try
            {
                user = _userService.GetById(id)?.ToMVCUser();
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            if (ReferenceEquals(user, null))
                RedirectToAction("Error", "Error");
            return View(user);
        }

        /// <summary>
        /// /Delete users Post
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.Delete(_userService.GetById(user.Id));
                }
                catch
                {
                    return RedirectToAction("Error", "Error");
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}