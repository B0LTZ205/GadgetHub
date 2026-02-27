using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;


namespace GadgetHub.Domain.Concrete
{
    public class EFGadgetRepository : IGadgetRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Gadget> Gadgets
        {
            get {  return context.Gadgets; }
        }

    }
}
