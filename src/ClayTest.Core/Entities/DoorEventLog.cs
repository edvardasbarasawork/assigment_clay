using System;

namespace ClayTest.Core.Entities
{
    public class DoorEventLog
    {
        public Guid Id { get; set; }

        public string Event { get; set; }

        public Guid UserId { get; set; }

        public Guid DoorId { get; set; }

        public DateTime Created { get; set; }
    }
}
