using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes
{
    [Serializable]
   public class Trip
    {
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}
