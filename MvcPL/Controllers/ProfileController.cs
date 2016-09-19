using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Interfaces;
using MvcPL.Infastructure.Mappers;
using MvcPL.ViewModels;

namespace MvcPL.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserInformationService _informationServiceService;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;
        public ProfileController(IUserInformationService userInformationService, IPhotoService photoService, IUserService userService)
        {
            if (ReferenceEquals(userInformationService, null))
                throw new ArgumentNullException(nameof(userInformationService));
            if (ReferenceEquals(photoService, null))
                throw new ArgumentNullException(nameof(photoService));
            if (ReferenceEquals(userService, null))
                throw new ArgumentNullException(nameof(userService));
            _informationServiceService = userInformationService;
            _photoService = photoService;
            _userService = userService;
        }

        /// <summary>
        /// Show profile user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ShowProfile(string userName, int page = 1)
        {
            IEnumerable<PhotoViewModel> photos = null;
            try
            {
                photos = _photoService.AllUserPhotos(userName).Select(t => t.ToMVCPhoto());
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            int pageSize = 12;

            var ivm = new IndexViewModel<PhotoViewModel>(page, pageSize, photos);
            var profile = new ProfileViewModel
            {
                UserInformation = _informationServiceService.GetByUserId(_userService.GetUserByName(userName).Id).ToMVCInformationUsers(),
                AllUserPhotos = ivm,
                UserName = userName
            };
            return View(profile);
        }

        /// <summary>
        /// Edit user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }

        /// <summary>
        /// Edit user profile Post
        /// </summary>

        public ActionResult EditProfile(EditUserInformation information, HttpPostedFileBase uploadImage)
        {
            UserInformationEntity profile = null;
            try
            {
                profile = _informationServiceService.GetByUserId(_userService.GetUserByName(User.Identity.Name).Id);
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }

            profile.Avatar = imageData;
            profile.FirstName = information.FirstName ?? profile.FirstName;
            profile.LastName = information.LastName ?? profile.LastName;
            profile.Age = information.Age == 0 ? profile.Age : information.Age;

            _informationServiceService.Update(profile);
            return RedirectToAction("ShowProfile", new { userName = User.Identity.Name });
        }
    }
}