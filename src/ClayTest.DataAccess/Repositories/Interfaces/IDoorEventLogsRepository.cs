using ClayTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.DataAccess.Repositories.Interfaces
{
    public interface IDoorEventLogsRepository
    {
        Task<IEnumerable<DoorEventLog>> GetAllAsync();

        Task<IEnumerable<DoorEventLog>> GetByDoorIdAsync(Guid doorId);

        Task<DoorEventLog> CreateAsync(DoorEventLog doorEventLog);
    }
}
