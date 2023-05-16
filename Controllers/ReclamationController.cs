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
                canEdit= reclamation.Status == ReclamationStatus.NotProcessed
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

    }
}
