using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P2EyeRIS.Models
{
    public class Student
    {
        [Display(Name = "ID")]
        public String Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }
    }
}
