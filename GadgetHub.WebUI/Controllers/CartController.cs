using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGadgetRepository repository;

        public CartController(IGadgetRepository repo)
        {
            repository = repo;
        }
        private Cart GetCar()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public RedirectToRouteResult AddToCart(int GadgetId, string returnUrl)
        {
            Gadget product = repository.Gadgets.FirstOrDefault(p => p.GadgetId == GadgetId);

            if (product != null)
            {
                GetCar().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int GadgetId, string returnUrl)
        {
            Gadget product = repository.Gadgets.FirstOrDefault(p => p.GadgetId == GadgetId);

            if (product != null)
            {
                GetCar().RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });

        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCar(), ReturnUrl = returnUrl });
        }
    }
}