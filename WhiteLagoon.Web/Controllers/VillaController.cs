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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                if (obj.Image != null)
                {
                    string filename=Guid.NewGuid().ToString()+Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath,@"images\VillaImages");
                    using var fileStream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create) ;
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\VillaImages\" + filename;


                }
                else
                { 
                    obj.ImageUrl= "https://placehold.co/600x400";
                }
                _unitOfWork.VillaRepo.AddRepo(obj);
                _unitOfWork.SaveUnitOfWork();
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
                if (obj.Image != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImages");                    
                                       
                    if (!string.IsNullOrEmpty(obj.ImageUrl))//Delete old image
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var fileStream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\VillaImages\" + filename;

                }                
                _unitOfWork.VillaRepo.UpdateRepo(obj);
                _unitOfWork.SaveUnitOfWork();
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
                if (!string.IsNullOrEmpty(recordTodelete.ImageUrl))//Delete old image
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, recordTodelete.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.VillaRepo.RemoveRepo(recordTodelete);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction("Index", "Villa");  
            }
            TempData["error"] = "The villa hasn't been deleted successfully.";
            return View(obj);
        }
        #endregion

    }
}
