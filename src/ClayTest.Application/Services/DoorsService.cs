using ClayTest.Application.Exceptions;
using ClayTest.Application.Services.Entities;
using ClayTest.Application.Services.Interfaces;
using ClayTest.Core.Entities;
using ClayTest.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClayTest.Application.Services
{
    public class DoorsService : IDoorsService
    {
        public readonly IClaimService _claimService;
        public readonly IDoorsRepository _doorsRepository;
        public readonly IDoorEventLogsRepository _doorEventLogsRepository;

        public DoorsService(IClaimService claimService, IDoorsRepository doorsRepository,
            IDoorEventLogsRepository doorEventLogsRepository)
        {
            _claimService = claimService;
            _doorsRepository = doorsRepository;
            _doorEventLogsRepository = doorEventLogsRepository;
        }

        public async Task<IEnumerable<Door>> GetAllAsync()
        {
            return await _doorsRepository.GetAllAsync();
        }

        public async Task<Door> GetByIdAsync(Guid id)
        {
            var door = await _doorsRepository.GetByIdAsync(id);

            if (door == null)
            {
                throw new NotFoundException($"Door with Id {id} was not found.");
            }

            return door;
        }

        public async Task<Door> PatchAsync(DoorPatch doorPatch)
        {
            var door = await _doorsRepository.GetByIdAsync(doorPatch.Id);

            if (door == null)
            {
                throw new NotFoundException($"Door with Id {doorPatch.Id} was not found.");
            }

            if (door.Closed != doorPatch.Closed.Value)
            {
                door.Closed = doorPatch.Closed.Value;

                await _doorEventLogsRepository.CreateAsync(new DoorEventLog()
                {
                    DoorId = door.Id,
                    UserId = new Guid(_claimService.GetUserId()),
                    Event = $"Door was {(doorPatch.Closed.Value ? "Closed" : "Opened")}",
                });

                return await _doorsRepository.UpdateAsync(door);
            }

            return door;
        }
    }
}
