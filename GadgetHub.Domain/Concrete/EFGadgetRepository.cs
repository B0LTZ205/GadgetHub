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

        public void SaveGadget(Gadget gadget)
        {
            if (gadget.GadgetId == 0)
            {
                context.Gadgets.Add(gadget);
            }

            else
            {
                Gadget dbEntry = context.Gadgets.Find(gadget.GadgetId);

                if (dbEntry != null)
                {
                    dbEntry.Name = gadget.Name;
                    dbEntry.Description = gadget.Description;
                    dbEntry.Price = gadget.Price;
                    dbEntry.Category = gadget.Category;

                    //image
                    dbEntry.ImageData = gadget.ImageData;
                    dbEntry.ImageMimeType = gadget.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Gadget DeleteGadget(int gadgetId)
        {
            Gadget dbEntry = context.Gadgets.Find(gadgetId);
            if (dbEntry != null)
            {
                context.Gadgets.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
