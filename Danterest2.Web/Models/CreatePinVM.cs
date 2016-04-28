using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Danterest2.Web.Models
{
    public class CreatePinVM
    {
        [Required]
        public string PhotoUrl { get; set; }

        public string LinkUrl { get; set; }

        public string Description { get; set; }

    }
}