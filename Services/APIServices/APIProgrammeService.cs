using FootballProgrammes.Data;
using FootballProgrammes.Dtos.Programmes;
using FootballProgrammes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace FootballProgrammes.Services
{
    public interface IAPIService
    {
        Task<ServiceResponse<IEnumerable<GetProgrammeDto>>> GetAllProgrammes();
    }

    public class APIProgrammeService : IAPIService
    {
        public APIProgrammeService(ApplicationDbContext context,
            IFootballProgrammeService footballProgrammeService,
            IMapper autoMapper)
        {
            Context = context;
            FootballProgrammeService = footballProgrammeService;
            AutoMapper = autoMapper;
        }

        public ApplicationDbContext Context { get; }
        public IFootballProgrammeService FootballProgrammeService { get; }
        public IMapper AutoMapper { get; }

        public async Task<ServiceResponse<IEnumerable<GetProgrammeDto>>> GetAllProgrammes()
        {
            var response = new ServiceResponse<IEnumerable<GetProgrammeDto>>();
            var programmes = await FootballProgrammeService.GetAllFootballProgrammes();
            if(programmes == null)
            {
                response.Success = false;
                response.Message = "Programmes Not Found";
                return response;
            }
            response.Data = programmes.Select(e=>AutoMapper.Map<GetProgrammeDto>(e)).ToList();
            return response;
        }
    }
}
