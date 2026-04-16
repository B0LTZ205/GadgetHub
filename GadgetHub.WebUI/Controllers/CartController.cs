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
        private IOrderProcessor orderProcessor;

        public CartController(IGadgetRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }
        private Cart cart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public RedirectToRouteResult AddToCart(Cart cart, int GadgetId, string returnUrl)
        {
            Gadget product = repository.Gadgets.FirstOrDefault(p => p.GadgetId == GadgetId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int GadgetId, string returnUrl)
        {
            Gadget product = repository.Gadgets.FirstOrDefault(p => p.GadgetId == GadgetId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });

        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty. Please add some items.");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}