using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace AricCar.Cars
{
    public class GetCarsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
    }
}
