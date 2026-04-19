using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IGadgetRepository repository;

        public AdminController(IGadgetRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Gadgets);
        }

        public ViewResult Edit (int gadgetId)
        {
            Gadget gadget = repository.Gadgets.FirstOrDefault(p => p.GadgetId == gadgetId);
            return View(gadget);
        }

        [HttpPost]
        public ActionResult Edit(Gadget gadget, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    gadget.ImageMimeType = image.ContentType;
                    gadget.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(gadget.ImageData, 0,
                    image.ContentLength);
                }

                repository.SaveGadget(gadget);

                TempData["message"] = string.Format
                    ("{0} has been saved", gadget.Name);

                return RedirectToAction("Index");
            }
            else
            {
                               // there is something wrong with the data values
                return View(gadget);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Create Gadget";

            return View("Edit", new Gadget());
        }

        [HttpPost]
        public ActionResult Delete(int gadgetId)
        {
            Gadget deletedGadget = repository.DeleteGadget(gadgetId);
            if (deletedGadget != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedGadget.Name);
            }
            return RedirectToAction("Index");
        }
    }

}