using FootballProgrammes.Models;
using FootballProgrammes.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballProgrammes.API_Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProgrammesController : ControllerBase
    {
        public ProgrammesController(IAPIService aPIService)
        {
            APIService = aPIService;
        }
        public IAPIService APIService { get; }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<FootballProgramme>>>> GetAllProgrammes()
        {
            return Ok(await APIService.GetAllProgrammes());
        }

    }
}
