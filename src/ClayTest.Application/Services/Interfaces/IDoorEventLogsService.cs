using ClayTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.Application.Services.Interfaces
{
    public interface IDoorEventLogsService
    {
        Task<IEnumerable<DoorEventLog>> GetAllAsync();

        Task<IEnumerable<DoorEventLog>> GetByDoorIdIdAsync(Guid doorId);
    }
}
