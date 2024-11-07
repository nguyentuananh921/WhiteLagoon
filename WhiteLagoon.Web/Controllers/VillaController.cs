using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db; 
        }

        public IActionResult Index()
        {
            var villars=_db.Villas.ToList();
                
            return View(villars);
        }
        public IActionResult Create()
        {
            return View();

            //return View("Index");
        }

        [HttpPost]
        public IActionResult Create(Villa objec)
        {
            _db.Villas.Add(objec);
            _db.SaveChanges();
            return RedirectToAction("Index","Villa");
            //return RedirectToAction("Index");
            //return View("Index"); //Error here
        }
    }
}
