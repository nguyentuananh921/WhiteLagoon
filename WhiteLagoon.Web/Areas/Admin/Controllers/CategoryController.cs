using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Domain.Entities.Bulky;

namespace WhiteLagoon.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Contructor
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public IActionResult Index()
        {
            var categories = _unitOfWork.CategoryRepo.GetAllRepo();
            return View(categories);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.AddRepo(obj);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The Category has been create successfully.";
                return RedirectToAction("Index", "Category");
            }
            TempData["error"] = "The Category has not been create successfully.";
            return View(obj);
        }

        #region Update
        [HttpGet]
        public IActionResult Update(int? categoryid)
        {
            Category? obj = _unitOfWork.CategoryRepo.GetRepo(needVilla => needVilla.Id == categoryid);
            if (categoryid == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepo.UpdateRepo(obj);

                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The category has been updated successfully.";
                return RedirectToAction("Index", "Category");
            }
            TempData["error"] = "The category hasn't been updated successfully.";
            return View(obj);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Delete(int? categoryid)
        {

            Category obj = _unitOfWork.CategoryRepo.GetRepo(needCate => needCate.Id == categoryid);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Category? recordTodelete = _unitOfWork.CategoryRepo.GetRepo(u => u.Id == obj.Id);
            if (recordTodelete is not null)
            {
                _unitOfWork.CategoryRepo.RemoveRepo(recordTodelete);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The category has been delete successfully.";
                return RedirectToAction("Index", "Category");
            }
            TempData["error"] = "The category hasn't been deleted successfully.";
            return View(obj);
        }
        #endregion
    }
}
