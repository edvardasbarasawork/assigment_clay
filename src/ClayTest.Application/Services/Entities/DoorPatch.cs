using System;
using System.ComponentModel.DataAnnotations;

namespace ClayTest.Application.Services.Entities
{
    public class DoorPatch
    {
        public Guid Id { get; set; }

        [Required]
        public bool? Closed { get; set; }
    }
}
