using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;

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
            //var villarsNumber=_db.VillaNumbers.ToList();

            //Loading Navigation Properties using Include.
            var villarsNumber = _db.VillaNumbers.Include(u=>u.Villa).ToList(); 

            return View(villarsNumber);
        }
        #region Create
        
        public IActionResult Create()
        {
            #region Using ViewBag
            //IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            ////ViewData["VillaList"] = list;//Viewdata is to move data from controller to view
            //ViewBag.VillaList = list;
            //return View();
            #endregion

            #region Using VM
            VillaNumberVM villaNumberVM = new VillaNumberVM()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            #endregion
            return View(villaNumberVM);

            //return View("Index");
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)

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

            //if (ModelState.IsValid)
            //{
            //    _db.VillaNumbers.Add(obj);
            //    _db.SaveChanges();
            //    TempData["success"] = "The villa number has been create successfully.";
            //    return RedirectToAction("Index", "VillaNumber");
            //    //return RedirectToAction("Index");
            //    //return View("Index"); //Error here
            //}

            bool roomNumberExist = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            //bool isNumberUnique = _db.VillaNumbers.Where(u => u.Villa_Number == obj.Villa_Number).Count()==0;
            if (ModelState.IsValid && !roomNumberExist)
            {
                
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            else
            {
                TempData["error"] = "The villa number has already Exist";
                obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }

            
            return View(obj);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int? villaNumberId)
        {
            #region Using VM
            VillaNumberVM villaNumberVM = new VillaNumberVM()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber=_db.VillaNumbers.FirstOrDefault(u=>u.Villa_Number== villaNumberId)
            };
            #endregion
            //Villa? obj = _db.Villas.FirstOrDefault(needVilla => needVilla.Id == villaNumberId);
            //Villa? obj2 = _db.Villas.Find(VillaId);
            //var villarList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 4).ToList();
            if (villaNumberVM.VillaNumber == null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Update(VillaNumberVM obj)
        {
            //bool roomNumberExist = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            //bool isNumberUnique = _db.VillaNumbers.Where(u => u.Villa_Number == obj.Villa_Number).Count()==0;
            if (ModelState.IsValid)
            {

                _db.VillaNumbers.Update(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been update successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? villaNumberId)
        {
            #region Using VM
            VillaNumberVM villaNumberVM = new VillaNumberVM()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
            #endregion
            //Villa? obj = _db.Villas.FirstOrDefault(needVilla => needVilla.Id == villaNumberId);
            //Villa? obj2 = _db.Villas.Find(VillaId);
            //var villarList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy > 4).ToList();
            if (villaNumberVM.VillaNumber == null)
            {
                //return NotFound();   
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM obj)
        {            
            VillaNumber? recordTodelete = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (recordTodelete is not null)
            {
                
                _db.VillaNumbers.Remove(recordTodelete);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been delete successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            else 
            {
                TempData["error"] = "The villa number hasn't been deleted successfully.";
                return View();
            }            
        }
        #endregion

    }
}
