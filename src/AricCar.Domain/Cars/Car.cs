using AricCar.Regions;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace AricCar.Cars
{
    public class Car : FullAuditedAggregateRoot<Guid>
    {
        public virtual string DistrctCode { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Type { get; set; }

        public virtual string? Description { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<CarImage> Images { get; set; } = [];
    }
}
