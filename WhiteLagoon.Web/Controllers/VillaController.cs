using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        #region Contructor
        private readonly IVillaRepository _villaRepo;
        public VillaController(IVillaRepository villaRepo)
        {
            _villaRepo = villaRepo;
        }
        #endregion
        public IActionResult Index()
        {
            var villars = _villaRepo.GetAllRepo();
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
                _villaRepo.AddRepo(obj);
                _villaRepo.SaveRepo();
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
            Villa? obj = _villaRepo.GetByIdRepo(needVilla => needVilla.Id == VillaId);
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
                _villaRepo.UpdateRepo(obj);
                _villaRepo.SaveRepo();
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
            Villa? obj = _villaRepo.GetByIdRepo(needVilla => needVilla.Id == VillaId);
            if (obj is null)
            { 
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
         Villa? recordTodelete = _villaRepo.GetByIdRepo(u => u.Id == obj.Id);
            if (recordTodelete is not null)
            {
                _villaRepo.RemoveRepo(recordTodelete);
                _villaRepo.SaveRepo();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction("Index", "Villa");  
            }
            TempData["error"] = "The villa hasn't been deleted successfully.";
            return View(obj);
        }
        #endregion

    }
}
