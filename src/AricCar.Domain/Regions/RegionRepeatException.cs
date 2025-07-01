using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace AricCar.Regions
{
    public class RegionRepeatException : BusinessException
    {
        public RegionRepeatException() : base(AricCarDomainErrorCodes.RegionAlreadyExists, "区域重复")
        {
        }
    }
}
