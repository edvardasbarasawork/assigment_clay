using ClayTest.Application.Services.Entities;
using ClayTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.Application.Services.Interfaces
{
    public interface IDoorsService
    {
        Task<Door> GetByIdAsync(Guid id);

        Task<IEnumerable<Door>> GetAllAsync();

        Task<Door> PatchAsync(DoorPatch doorPatch);
    }
}
