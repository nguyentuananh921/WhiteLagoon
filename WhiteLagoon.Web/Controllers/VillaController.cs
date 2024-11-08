using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        #region Contructor
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        public IActionResult Index()
        {
            var villars=_db.Villas.ToList();
                
            return View(villars);
        }
        #region Create
        
        public IActionResult Create()
        {
            return View();

            //return View("Index");
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
                _db.Villas.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            return View(obj);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int? VillaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(needVilla => needVilla.Id == VillaId);
            //Villa? obj2 = _db.Villas.Find(VillaId);
            //var villarList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 4).ToList();
            if (VillaId == null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            //Custome Validation
            //if (obj.Name == obj.Description)
            //{
            //    //ModelState.AddModelError("description", "The description cannot exactly match the Name.");
            //    ModelState.AddModelError("", "The description cannot exactly match the Name.");
            //}
            if (ModelState.IsValid)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            return View(obj);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? VillaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(needVilla => needVilla.Id == VillaId);
            //Villa? obj2 = _db.Villas.Find(VillaId);
            //var villarList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 4).ToList();
            if (VillaId == null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }

            return View(obj);
        }
        //[HttpPost]
        //public IActionResult Update(Villa obj)
        //{
        //    //Custome Validation
        //    //if (obj.Name == obj.Description)
        //    //{
        //    //    //ModelState.AddModelError("description", "The description cannot exactly match the Name.");
        //    //    ModelState.AddModelError("", "The description cannot exactly match the Name.");
        //    //}
        //    if (ModelState.IsValid)
        //    {
        //        _db.Villas.Update(obj);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index", "Villa");
        //        //return RedirectToAction("Index");
        //        //return View("Index"); //Error here
        //    }
        //    return View(obj);
        //}
        #endregion

    }
}
