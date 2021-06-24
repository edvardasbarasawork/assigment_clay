using ClayTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.DataAccess.Repositories.Interfaces
{
    public interface IDoorsRepository
    {
        Task<Door> GetByIdAsync(Guid id);

        Task<Door> UpdateAsync(Door door);

        Task<IEnumerable<Door>> GetAllAsync();
    }
}
