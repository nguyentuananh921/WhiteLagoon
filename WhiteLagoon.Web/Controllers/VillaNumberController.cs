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
    public class VillaNumberController : Controller
    {
        #region Contructor
        private readonly IUnitOfWork _unitOfWork;        
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public IActionResult Index()
        {
            //var villarsNumber=_db.VillaNumbers.ToList();

            //Loading Navigation Properties using Include.
            var villarsNumber = _unitOfWork.VillaNumberRepo.GetAllRepo(includeProperties:"Villa"); 

            return View(villarsNumber);
        }
        #region Create
        
        public IActionResult Create()
        {
            

            #region Using VM
            VillaNumberVM villaNumberVM = new VillaNumberVM()
            {
                VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
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
            
            //bool roomNumberExist = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            bool roomNumberExist = _unitOfWork.VillaNumberRepo.AnyRepo(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            //bool isNumberUnique = _db.VillaNumbers.Where(u => u.Villa_Number == obj.Villa_Number).Count()==0;
            if (ModelState.IsValid && !roomNumberExist)
            {
                
                //_db.VillaNumbers.Add(obj.VillaNumber);
                //_db.SaveChanges();
                _unitOfWork.VillaNumberRepo.AddRepo(obj.VillaNumber);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            else
            {
                TempData["error"] = "The villa number has already Exist";
                //obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                obj.VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
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
                VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                //VillaNumber=_db.VillaNumbers.FirstOrDefault(u=>u.Villa_Number== villaNumberId)
                VillaNumber = _unitOfWork.VillaNumberRepo.GetRepo(u => u.Villa_Number == villaNumberId)
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
            bool roomNumberExist = _unitOfWork.VillaNumberRepo.AnyRepo(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (ModelState.IsValid)
            {

                //_db.VillaNumbers.Update(obj.VillaNumber);
                //_db.SaveChanges();
                _unitOfWork.VillaNumberRepo.UpdateRepo(obj.VillaNumber);
                _unitOfWork.SaveUnitOfWork();
                TempData["success"] = "The villa number has been update successfully.";
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "VillaNumber");
                //return RedirectToAction("Index");
                //return View("Index"); //Error here
            }
            obj.VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
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
                VillaList = _unitOfWork.VillaRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumberRepo.GetRepo(u => u.Villa_Number == villaNumberId)
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
            VillaNumber? recordTodelete = _unitOfWork.VillaNumberRepo.GetRepo(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            if (recordTodelete is not null)
            {
                
                //_db.VillaNumbers.Remove(recordTodelete);
                //_db.SaveChanges();
                _unitOfWork.VillaNumberRepo.RemoveRepo(recordTodelete);
                _unitOfWork.SaveUnitOfWork();
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
