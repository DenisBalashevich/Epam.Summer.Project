using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// When unknown action
        /// </summary>
        /// <param name="actionName"></param>
        protected override void HandleUnknownAction(string actionName)
        {
            try
            {
                this.View(actionName).ExecuteResult(this.ControllerContext);
            }
            catch (Exception)
            {
                this.RedirectToAction("Index","Home").ExecuteResult(this.ControllerContext);
            }

        }


        /// <summary>
        ///Error page
        /// </summary>
        /// <returns> Erro view.</returns>
        public ActionResult Error()
        {
            return View();
        }
    }
}