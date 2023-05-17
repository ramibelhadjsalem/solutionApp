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
                reclamation.Status = ReclamationStatus.Resolved;
                var resultOfRec= await _reclamationRepository.Update(reclamation);
                return LocalRedirect("~/Reclamation/Detail/" + id);
            }
          

            return LocalRedirect("~/Reclamation/Detail/"+id);
        }
      
        [Authorize(Policy = "RequireTechnicienRole")]
        public async Task<IActionResult> NoSolution(int id)
        {
            var reclamation = await _reclamationRepository.Get(id);
            if (reclamation == null) return LocalRedirect("~/Error/notFound");
            if (reclamation.TechUserId != User.GetUserId()) return LocalRedirect("~/Error/Frobiden");

            reclamation.Status = ReclamationStatus.NoSolution;

            var result = await _reclamationRepository.Update(reclamation);
            return LocalRedirect("~/Reclamation/Detail/" + id);


        }
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> SolutionWorking(int id)
        {
            var solution =await _solutionRepository.getInfo(id);
            if (solution == null) return LocalRedirect("~/Error/notFound");
            if (solution.Reclamation.UserId != User.GetUserId()) return LocalRedirect("~/Error/Frobiden");

            solution.status = SolutionStatus.Working;
            var result = await _solutionRepository.Update(solution);

            var reclamation = solution.Reclamation;
            reclamation.Status = ReclamationStatus.Resolved;
            var result2 = await _reclamationRepository.Update(reclamation);


        
            return LocalRedirect("~/Reclamation/Detail/" + reclamation.Id);

            


        }
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> SolutionNotWorking(int id)
        {
            var solution = await _solutionRepository.getInfo(id);
            if (solution == null) return LocalRedirect("~/Error/notFound");
            if (solution.Reclamation.UserId != User.GetUserId()) return LocalRedirect("~/Error/Frobiden");

            solution.status = SolutionStatus.NotWorking;
            var result = await _solutionRepository.Update(solution);

            var reclamation = solution.Reclamation;
            reclamation.Status = ReclamationStatus.InProcess;
            var result2 = await _reclamationRepository.Update(reclamation);



            return LocalRedirect("~/Reclamation/Detail/" +reclamation.Id);

        }

    }
}
