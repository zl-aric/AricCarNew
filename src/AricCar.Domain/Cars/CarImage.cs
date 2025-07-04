using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AricCar.Cars
{
    public class CarImage : CreationAuditedAggregateRoot<Guid>
    {
        public string Url { get; set; }
    }
}
