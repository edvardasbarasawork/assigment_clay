using ClayTest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClayTest.API.Controllers
{
    [Authorize]
    public class DoorEventLogsController : ApiController
    {
        public readonly IDoorEventLogsService _doorEventLogsService;

        public DoorEventLogsController(IDoorEventLogsService doorEventLogsService)
        {
            _doorEventLogsService = doorEventLogsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _doorEventLogsService.GetAllAsync());
        }
    }
}
