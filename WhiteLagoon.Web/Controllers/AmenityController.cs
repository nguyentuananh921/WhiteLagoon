using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Domain.Entities.WhiteLagoon;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels.WhiteLagoon;

namespace WhiteLagoon.Web.Controllers
{
    public class AmenityController : Controller
    {
        #region Contructor
        private readonly IUnitOfWork _unitOfWork;        
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public IActionResult Index()
        {
            //Loading Navigation Properties using Include.
            //var amenity = _unitOfWork.AmenityRepo.GetAllRepo(includeProperties:"Villa"); 
            var amenities = _unitOfWork.AmenityRepo.GetAllRepo(includeProperties: "Villa");
            return View(amenities);
        }
        #region Create
        
        public IActionResult Create()
        {            
            AmenityVM amenityvm = new AmenityVM()
            {
                VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };            
            return View(amenityvm);
            //return View("Index");
        }
        [HttpPost]
        public IActionResult Create(AmenityVM obj)

        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepo.AddRepo(obj.Amenity);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The Amenity has been create successfully.";
                return RedirectToAction(nameof(Index));
            }
            else //Reload data if fail to add
            {                
                obj.VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }            
        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Update(int? amenityId)
        {
            AmenityVM amenityVM = new AmenityVM()
            {
                VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.AmenityRepo.GetRepo(u => u.Id == amenityId)
            };
            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Update(AmenityVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepo.UpdateRepo(obj.Amenity);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The Amenity has been update successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "Amenity");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            else 
            { 
                obj.VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }            
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? amenityId)
        {
            AmenityVM amenityVM = new AmenityVM()
            {
                VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.AmenityRepo.GetRepo(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }
            else 
            {
                return View(amenityVM);
            }            
        }
        [HttpPost]
        public IActionResult Delete(AmenityVM obj)
        {            
            Amenity? recordTodelete = _unitOfWork.AmenityRepo.GetRepo(u => u.Id == obj.Amenity.Id);
            if (recordTodelete is not null)
            {                
                _unitOfWork.AmenityRepo.RemoveRepo(recordTodelete);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The Amenity has been delete successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "Amenity");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            else 
            {
                TempData["error"] = "The Amenity hasn't been deleted successfully.";
                return View();
            }            
        }
        #endregion

    }
}
