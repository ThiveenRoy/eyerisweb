using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P2EyeRIS.Models
{
    public class Staff
    {
        [Display(Name = "ID")]
        public String Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }
    }
}
