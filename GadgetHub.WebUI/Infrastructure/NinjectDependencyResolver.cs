using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moq;
using Ninject;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using Ninject.Planning.Targets;
using GadgetHub.Domain.Concrete;
using System.Configuration;

namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel mykernel;

        public NinjectDependencyResolver(IKernel kernelparam)
        {
            mykernel = kernelparam;
            AddBindings();
        }

        public object GetService(Type myserviceType)
        {
            return mykernel.TryGet(myserviceType);
        }

        public IEnumerable<object> GetServices(Type myserviceType)
        {
            return mykernel.GetAll(myserviceType);
        }

        private void AddBindings()
        {
/*            Mock<IGadgetRepository> myMock = new Mock<IGadgetRepository>();
            myMock.Setup(m => m.Gadget).Returns(new List<Gadgets>
            {
                new Gadgets { GadgetId=1, Name="Smartwatch X1", Brand="Nova", Price=199.99m, Description="Fitness + notifications", Category="Wearables" },
                new Gadgets { GadgetId=2, Name="NoiseCancel Pro", Brand="Sonic", Price=149.50m, Description="Over-ear ANC headphones", Category="Audio" },
                new Gadgets { GadgetId=3, Name="Pocket Drone Mini", Brand="SkyBit", Price=89.99m, Description="Beginner-friendly drone", Category="Drones" },
                new Gadgets { GadgetId=4, Name="4K Action Cam", Brand="GoPeak", Price=129.00m, Description="Waterproof action camera", Category="Cameras" }
            });
            mykernel.Bind<IGadgetRepository>().ToConstant(myMock.Object);*/

            mykernel.Bind<IGadgetRepository>().To<EFGadgetRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse
                    (ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            mykernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

        }
    }
}