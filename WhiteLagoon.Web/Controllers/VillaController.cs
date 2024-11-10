using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        #region Contructor
        private readonly IUnitOfWork _unitOfWork;
        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public IActionResult Index()
        {
            var villars = _unitOfWork.VillaRepo.GetAllRepo();
            return View(villars);
        }
        #region Create
        
        public IActionResult Create()
        {
            return View();            
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                //ModelState.AddModelError("description", "The description cannot exactly match the Name.");
                ModelState.AddModelError("", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                //_db.Villas.Add(obj);
                //_db.SaveChanges();
                _unitOfWork.VillaRepo.AddRepo(obj);
                _unitOfWork.VillaRepo.SaveRepo();
                TempData["success"] = "The villa has been create successfully.";
                return RedirectToAction("Index", "Villa");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            TempData["error"] = "The villa has not been create successfully.";
            return View(obj);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int? VillaId)
        {
            Villa? obj = _unitOfWork.VillaRepo.GetRepo(needVilla => needVilla.Id == VillaId);
            if (VillaId == null)
            {                
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {            
            if (ModelState.IsValid)
            {                
                _unitOfWork.VillaRepo.UpdateRepo(obj);
                _unitOfWork.VillaRepo.SaveRepo();
                TempData["success"] = "The villa has been updated successfully.";
                return RedirectToAction("Index", "Villa");              
            }
            TempData["error"] = "The villa hasn't been updated successfully.";
            return View(obj);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Delete(int? VillaId)
        {
            Villa? obj = _unitOfWork.VillaRepo.GetRepo(needVilla => needVilla.Id == VillaId);
            if (obj is null)
            { 
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
         Villa? recordTodelete = _unitOfWork.VillaRepo.GetRepo(u => u.Id == obj.Id);
            if (recordTodelete is not null)
            {
                _unitOfWork.VillaRepo.RemoveRepo(recordTodelete);
                _unitOfWork.VillaRepo.SaveRepo();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction("Index", "Villa");  
            }
            TempData["error"] = "The villa hasn't been deleted successfully.";
            return View(obj);
        }
        #endregion

    }
}
