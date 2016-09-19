using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Interfaces;
using MvcPL.ViewModels;
using System.IO;
using BLL.Interfaces.Entities;
using BLL.Services;
using MvcPL.Infastructure.Mappers;
using PagedList;

namespace MvcPL.Controllers
{
    [AllowAnonymous]
    public class HomeController : ErrorController
    {
        private readonly IPhotoService _photoService;
        private readonly ITagService _tagService;

        public HomeController(IPhotoService service, ITagService tagService)
        {
            this._photoService = service;
            this._tagService = tagService;
        }
        /// <summary>
        /// Display index page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1)
        {
            List<PhotoViewModel> photos = null;
            try
            {
               photos = _photoService.GetAll().Select(photo => photo.ToMVCPhoto()).ToList();
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            int pageSize = 1;

            var ivm = new IndexViewModel<PhotoViewModel>(page, pageSize, photos);

            return View(ivm);
        }
        /// <summary>
        /// Create photo
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// create photo Post
        /// </summary>

        [Authorize]
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase uploadImage, string tags, string name)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                PhotoViewModel photo = new PhotoViewModel()
                {
                    Date = DateTime.Now,
                    Picture = imageData,
                    Name = name,
                    Tags = new List<TagViewModel>(TagParser(tags)),
                    Ratings = new List<RatingViewModel>(),
                    UserName = User.Identity.Name
                };
                try
                {
                    _photoService.Create(photo.ToBLLPhoto());
                }
                catch
                {
                    return RedirectToAction("Error", "Error");
                }
                RedirectToAction("Index");
            }
            return View();

        }

        /// <summary>
        /// Parse tags
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        private static List<TagViewModel> TagParser(string tags)
        {
            if (ReferenceEquals(tags, null))
                return null;
            var list = new List<TagViewModel>();
            foreach (var str in tags.Split('#'))
            {
                if (str == String.Empty)
                    continue;
                list.Add(new TagViewModel() { Name = str });
            }
            return list;
        }

        /// <summary>
        /// Deleting photo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            PhotoViewModel photo = null;
            try
            {
                photo = _photoService.GetById(id)?.ToMVCPhoto();
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            if (ReferenceEquals(photo, null))
                RedirectToAction("Error", "Error");
            return View(photo);
        }

        /// <summary>
        /// Delete photo Post
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Delete(PhotoViewModel photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _photoService.Delete(_photoService.GetById(photo.Id));
                }
                catch
                {
                    return RedirectToAction("Error", "Error");
                }
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        /// <summary>
        /// Set Rating
        /// </summary>
        /// <param name="idPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Rating(int idPhoto)
        {
            var rating = new RatingEntity()
            {
                UserName = User.Identity.Name,
                PhotoId = idPhoto
            };
            PhotoEntity photo = null;
            try
            {
                 photo = _photoService.GetById(idPhoto);
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            var photoRating = new PhotoInformViewModel { PhotoId = photo.Id };
            if (ReferenceEquals(photo.Ratings.FirstOrDefault(p => p.UserName == rating.UserName), null))
            {
                _photoService.AddRating(rating);
                photoRating.IsSelected = true;
            }
            else
            {
                _photoService.DeleteRating(rating);
                photoRating.IsSelected = false;
            }
            try
            {
                photoRating.RatingsCount = _photoService.GetById(photo.Id).Ratings.Count;
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            return Json(photoRating);
        }

        /// <summary>
        /// Find Tags
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult TagFinder(string tagName, int page = 1)
        {
            int pageSize = 10;
            IEnumerable<PhotoViewModel> tags = null;
            try
            {
                 tags = _tagService.GetTagByName(tagName)?.Photos.Select(a => a.ToMVCPhoto());
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            var ivm = new IndexViewModel<PhotoViewModel>(page, pageSize, tags);
            if (Request.IsAjaxRequest())
            {
                if (ReferenceEquals(tags, null))
                {
                    return Json("Sorry, we can't find information ", JsonRequestBehavior.AllowGet);
                }
                return PartialView("ShowPhotos", ivm);
            }
            return View("Index", ivm);
        }

        /// <summary>
        /// Tag name helper
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult TagNameFinder(string term)
        {
            IEnumerable<TagViewModel> projection = null;
            try
            {
                projection = _tagService.FindTags(term).Select(t => t.ToMVCTag());
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}