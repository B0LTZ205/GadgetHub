using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your State")]
        public string State { get; set; }
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter your Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }



    }
}
