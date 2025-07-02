using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AricCar.Regions
{
    public class RegionCreateDto
    {
        [Required]
        public RegionItem Province { get; set; }

        [Required]
        public RegionItem City { get; set; }

        [Required]
        public RegionItem District { get; set; }
    }
}
