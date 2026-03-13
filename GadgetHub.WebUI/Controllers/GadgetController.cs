using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {

        private IGadgetRepository myrepository;

        public GadgetController(IGadgetRepository productrepository)
        {
            this.myrepository = productrepository;
        }

        public int pageSize = 4;
        public ViewResult List(string category,int page = 1)
        {
            GadgetsListViewModel model = new GadgetsListViewModel
            {
                Gadgets = myrepository.Gadgets.Where(g => category == null || g.Category == category).OrderBy(g => g.GadgetId).Skip((page - 1) * pageSize).Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null? myrepository.Gadgets.Count() : myrepository.Gadgets.Where(g => g.Category == category).Count()
                },
                CurrentCategory = category

            };
            return View(model);
        }
    }
}