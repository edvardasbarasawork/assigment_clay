using ClayTest.Application.Services.Entities;
using ClayTest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClayTest.API.Controllers
{
    [Authorize]
    public class DoorsController : ApiController
    {
        public readonly IDoorsService _doorsService;
        public readonly IDoorEventLogsService _doorEventLogsService;

        public DoorsController(IDoorsService doorsService,
            IDoorEventLogsService doorEventLogsService)
        {
            _doorsService = doorsService;
            _doorEventLogsService = doorEventLogsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _doorsService.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var door = await _doorsService.GetByIdAsync(id);

            return Ok(door);
        }

        [HttpGet]
        [Route("{id}/logs")]
        public async Task<IActionResult> GetLogsByDoorIdAsync([FromRoute] Guid id)
        {
            return Ok(await _doorEventLogsService.GetByDoorIdIdAsync(id));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchAsync([FromRoute] Guid id, [FromBody] DoorPatch doorPatch)
        {
            doorPatch.Id = id;

            return Ok(await _doorsService.PatchAsync(doorPatch));
        }

        [HttpPatch]
        [Route("{id}/open")]
        public async Task<IActionResult> OpenDoorAsync([FromRoute] Guid id)
        {
            var doorPatch = new DoorPatch()
            {
                Id = id,
                Closed = false
            };

            return Ok(await _doorsService.PatchAsync(doorPatch));
        }

        [HttpPatch]
        [Route("{id}/close")]
        public async Task<IActionResult> CloseDoorAsync([FromRoute] Guid id)
        {
            var doorPatch = new DoorPatch()
            {
                Id = id,
                Closed = true
            };

            return Ok(await _doorsService.PatchAsync(doorPatch));
        }
    }
}
