using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using solutionApp.Data.Entities;
using solutionApp.Data.Repositories.ReclamationRepo;
using solutionApp.Data.Repositories.SolutionRepo;
using solutionApp.Extentions;
using solutionApp.Models;

namespace solutionApp.Controllers
{
    public class SolutionController : Controller
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly IReclamationRepository _reclamationRepository;

        public SolutionController(ISolutionRepository solutionRepository,IReclamationRepository reclamationRepository)
        {
            _solutionRepository = solutionRepository;
            _reclamationRepository = reclamationRepository;
        }

        [HttpPost]
        [Authorize(Policy = "RequireTechnicienRole")]
        public async Task<IActionResult> AddSolution(int id ,SolutionDto solutionDto)
        {   
            if(ModelState.IsValid)
            {
                var reclamation =await _reclamationRepository.Get(id);
                if (reclamation == null) return LocalRedirect("~/Error/notFound");

                if(reclamation.TechUserId != User.GetUserId()) return LocalRedirect("~/Error/Frobiden");

                var solution = new Solution()
                {
                    Title = solutionDto.title,
                    Description = solutionDto.description,
                    ReclamationId = reclamation.Id,
                    Reclamation = reclamation
                };
                var result= await _solutionRepository.Create(solution);
                return LocalRedirect("~/Reclamation/Detail/" + id);
            }
          

            return LocalRedirect("~/Reclamation/Detail/"+id);
        }
    }
}
