using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GadgetHub.Domain.Entities
{
    public class Gadget    {

        [HiddenInput(DisplayValue = false)]
        public int GadgetId { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a brand name")]
        public string Brand { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a category")]
        public string Category
        {
            get; set;
        }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
