using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        #region Contructor
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        public IActionResult Index()
        {
            var villarsNumber=_db.VillaNumbers.ToList();
                
            return View(villarsNumber);
        }
        #region Create
        
        public IActionResult Create()
        {
            return View();

            //return View("Index");
        }

        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            //if (obj.Name == obj.Description)
            //{
            //    //ModelState.AddModelError("description", "The description cannot exactly match the Name.");
            //    ModelState.AddModelError("", "The description cannot exactly match the Name.");
            //}
            #region Solution 1
            //ModelState.Remove("Villa");
            #endregion
            #region Solution 2
            //Modify Domain project 
            //< ItemGroup >
            //< FrameworkReference Include = "Microsoft.AspNetCore.App" ></ FrameworkReference >
            //</ ItemGroup >
            #endregion

            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            TempData["error"] = "The villa has number not been create successfully.";
            return View(obj);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int? villaNumberId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(needVilla => needVilla.Id == villaNumberId);
            //Villa? obj2 = _db.Villas.Find(VillaId);
            //var villarList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 4).ToList();
            if (villaNumberId == null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(VillaNumber obj)
        {
            //Custome Validation
            //if (obj.Name == obj.Description)
            //{
            //    //ModelState.AddModelError("description", "The description cannot exactly match the Name.");
            //    ModelState.AddModelError("", "The description cannot exactly match the Name.");
            //}
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been updated successfully.";
                return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            TempData["error"] = "The villa number hasn't been updated successfully.";
            return View(obj);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? villaNumberId)
        {
            VillaNumber? obj = _db.VillaNumbers.FirstOrDefault(needVillaNumber => needVillaNumber.Villa_Number == villaNumberId);
            //Villa? obj2 = _db.Villas.Find(VillaId);
            //var villarList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 4).ToList();
            if (obj is null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumber obj)
        {
            //Custome Validation
            //if (obj.Name == obj.Description)
            //{
            //    //ModelState.AddModelError("description", "The description cannot exactly match the Name.");
            //    ModelState.AddModelError("", "The description cannot exactly match the Name.");
            //}
            VillaNumber? recordTodelete = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == obj.Villa_Number);
            if (recordTodelete is not null)
            {
                
                _db.VillaNumbers.Remove(recordTodelete);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been delete successfully.";
                return RedirectToAction("Index", "Villa");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            TempData["error"] = "The villa number hasn't been deleted successfully.";
            return View(obj);
        }
        #endregion

    }
}
