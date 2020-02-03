using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P2EyeRIS.Models
{
    public class StudentGrade
    {
        [Display(Name = "Present")]
        public String P { get; set; }

        [Display(Name = "Late")]
        public String L { get; set; }

        [Display(Name = "Excused)]
        public String E { get; set; }

        [Display(Name = "Unexcused")]
        public String U { get; set; }
    }
}
