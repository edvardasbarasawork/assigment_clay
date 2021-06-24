using System;

namespace ClayTest.Core.Entities
{
    public class Door
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CompanyId { get; set; }

        public bool Closed { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
