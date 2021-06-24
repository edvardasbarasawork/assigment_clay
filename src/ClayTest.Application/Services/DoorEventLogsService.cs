using ClayTest.Application.Exceptions;
using ClayTest.Application.Services.Interfaces;
using ClayTest.Core.Entities;
using ClayTest.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.Application.Services
{
    public class DoorEventLogsService : IDoorEventLogsService
    {
        public readonly IDoorsRepository _doorsRepository;
        public readonly IDoorEventLogsRepository _doorEventLogsRepository;

        public DoorEventLogsService(IDoorsRepository doorsRepository,
            IDoorEventLogsRepository doorEventLogsRepository)
        {
            _doorsRepository = doorsRepository;
            _doorEventLogsRepository = doorEventLogsRepository;
        }

        public async Task<IEnumerable<DoorEventLog>> GetAllAsync()
        {
            return await _doorEventLogsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<DoorEventLog>> GetByDoorIdIdAsync(Guid doorId)
        {
            var door = await _doorsRepository.GetByIdAsync(doorId);

            if (door == null)
            {
                throw new NotFoundException($"Door with Id {doorId} was not found.");
            }

            return await _doorEventLogsRepository.GetByDoorIdAsync(doorId);
        }
    }
}
