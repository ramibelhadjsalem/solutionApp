using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using solutionApp.Data.Entities;
using solutionApp.Data.Repositories.ReclamationRepo;
using solutionApp.Data.Repositories.UserRepository;
using solutionApp.Extentions;
using solutionApp.Models;

namespace solutionApp.Controllers
{
    [Authorize]
    public class ReclamationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReclamationRepository _reclamationRepository;

        public ReclamationController(IUserRepository userRepository, IReclamationRepository reclamationRepository)
        {
            _userRepository = userRepository;
            _reclamationRepository = reclamationRepository;
        }

        public async Task<IActionResult> Index([FromQuery] Filter filterParam)
        {
            if (User.IsInRole("Technicien"))
            {
                var model = new ReclamationTechViewModel
                {
                    users = await _userRepository.GetAllByRole("User"),
                    filter = filterParam,
                    reclamations =await _reclamationRepository.getAllWithParams(filterParam)
                };

                return View("ReclamationsTech", model);
            }
            if (User.IsInRole("User"))
            {
                filterParam.UserId = User.GetUserId();
                var model = new ReclamationUserViewModel()
                {
                    filter = filterParam,
                    reclamations = await _reclamationRepository.getAllWithParams(filterParam)
                };
                return View("ReclamationsUser",model);
            }
            return LocalRedirect("~/Error/Forbiden");
        }
        public async Task<IActionResult> Detail(int id)
        {
            var reclamation = await _reclamationRepository.getRecalamationDeatils(id);
            if (reclamation == null) return LocalRedirect("~/error/notfound");
            var reclamationDetail = new ReclamationDetail()
            {
                reclamation = reclamation,
                isOwner = User.GetUserId() == reclamation.UserId,
                canEdit= reclamation.Status == ReclamationStatus.NotProcessed,
                canTake = User.IsInRole("Technicien") && reclamation.TechUserId ==null,
                canAddSolution = reclamation.TechUserId ==User.GetUserId() && User.IsInRole("Technicien") && reclamation.Status ==ReclamationStatus.InProcess
            };
            return View(reclamationDetail);
        }
        [HttpGet]
        [Authorize(Policy = "RequireUserRole")]
        public IActionResult AddReclamation()
        {
            return View();  
        }


        [HttpPost]
        [Authorize(Policy = "RequireUserRole")]
        public async Task<IActionResult> AddReclamation(ReclamationDto reclamation)
        {
            if(ModelState.IsValid)
            {
                var newReclamation = new Reclamation()
                {
                    Title = reclamation.title,
                    Description = reclamation.description,
                    UserId =User.GetUserId()

                };
                var result = await _reclamationRepository.Create(newReclamation);
                if(result !=null)
                {
                    return LocalRedirect("~/Reclamation");
                }
                else
                {
                    ModelState.AddModelError("", "thomething went wrong");
                    return View(reclamation);
                }
            }
            return View(reclamation);
        }

        public async Task<IActionResult> DeleteReclamation(int id)
        {
            var result = await _reclamationRepository.Get(id);
            if(result!=null && User.GetUserId() ==result.Id)
            {
                await _reclamationRepository.Delete(id);
                return LocalRedirect("~/reclamation");
            }
            return LocalRedirect("~/Error/Forbiden");
        }

        [Authorize(Policy = "RequireTechnicienRole")]
        public async Task<IActionResult> TakeReclamation(int id)
        {
            var reclamation = await _reclamationRepository.Get(id);
            if (reclamation == null) return LocalRedirect("~/Error/notfound");

            if(reclamation.TechUserId == null)
            {
                reclamation.TechUserId = User.GetUserId();
                reclamation.Status = ReclamationStatus.InProcess;
                await _reclamationRepository.Update(reclamation);
                return LocalRedirect("~/Reclamation/Detail/" + reclamation.Id);
            }
            return LocalRedirect("~/Error/Forbiden");

        }

    }
}
