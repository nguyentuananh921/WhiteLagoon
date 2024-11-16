using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities.Bulky;
using WhiteLagoon.Web.ViewModels.Bulky;

namespace WhiteLagoon.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var objProductList = _unitOfWork.ProductRepo.GetAllRepo(includeProperties: "Category").ToList();
            //Convert IEnumberable to SelectListItemt using projection in EF Core to select some column not all column
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepo
                .GetAllRepo().Select(u=>new SelectListItem 
            { 
                Text = u.Name,
                Value=u.Id.ToString()
            });

            return View(objProductList);
        }
        public IActionResult Upsert(int? productId)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.CategoryRepo.GetAllRepo()
                    .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (productId == null || productId == 0) //create
            {                
                return View(productVM);
            }
            else //update
            {
                //productVM.Product = _unitOfWork.ProductRepo.GetRepo(u => u.Id == productId, includeProperties: "ProductImages");
                productVM.Product = _unitOfWork.ProductRepo.GetRepo(u => u.Id == productId);
                return View(productVM);
            }
        }
        [HttpPost]
        //public IActionResult Upsert(ProductVM productVM, List<IFormFile> imageFiles) //When you want to save multiple image ralated to product
        public IActionResult Upsert(ProductVM productVM, IFormFile imageFiles)
        {
            if (ModelState.IsValid)
            {
                #region Save only one Image to Producttable
                if (imageFiles != null) //User want to update Image
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFiles.FileName);
                    string productPath = @"images\products\product-" + productVM.Product.Id;
                    string finalPath = Path.Combine(wwwRootPath, productPath);

                    if (!Directory.Exists(finalPath)) //Make sure finalPath exists
                    {
                        Directory.CreateDirectory(finalPath);
                    }
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl)) //Delete old  Image.
                    {                        
                        var oldImagePath = 
                            Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                    }
                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        imageFiles.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\" + productPath + @"\" + fileName;
                }  
                #endregion
                #region Save Product or Update Product
                if (productVM.Product.Id == 0)  //Create new Product
                {
                    _unitOfWork.ProductRepo.AddRepo(productVM.Product);
                }
                else  //Update Product
                {
                    _unitOfWork.ProductRepo.UpdateRepo(productVM.Product);
                }
                _unitOfWork.SaveUnitOfWork();
                #endregion
                #region Using Image table to save Image
                //_unitOfWork.SaveUnitOfWork();
                //string wwwRootPath = _webHostEnvironment.WebRootPath;
                //if (imageFiles != null)  //New File Is Selected
                //{
                //    foreach (IFormFile file in imageFiles)
                //    {
                //        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                //        string productPath = @"images\products\product-" + productVM.Product.Id;
                //        string finalPath = Path.Combine(wwwRootPath, productPath);

                //        if (!Directory.Exists(finalPath))
                //        {
                //            Directory.CreateDirectory(finalPath);
                //        }
                //        else 
                //        {
                //            using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                //            {
                //                file.CopyTo(fileStream);
                //            }
                //            //ProductImage productImage = new()
                //            //{
                //            //    ImageUrl = @"\" + productPath + @"\" + fileName,
                //            //    ProductId = productVM.Product.Id,
                //            //};
                //            //if (productVM.Product.ProductImages != null)
                //            //{
                //            //    productVM.Product.ProductImages = new List<ProductImage>();
                //            //    productVM.ProductImages.Add(productImage);
                //            //}
                //            productVM.Product.ImageUrl = @"\" + productPath + @"\" + fileName;
                //        }   
                //    }
                //    _unitOfWork.ProductRepo.UpdateRepo(productVM.Product);
                //    _unitOfWork.SaveUnitOfWork();
                //}
                #endregion
                TempData["success"] = "Product created/updated successfully";
                return RedirectToAction("Index");
            }
            else //Populate dropdown list again when return if model is not valid
            {
                productVM.CategoryList = _unitOfWork.CategoryRepo.GetAllRepo().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }
        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.ProductImageRepo.GetRepo(u => u.Id == imageId);
            int productId = imageToBeDeleted.ProductId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath =
                                   Path.Combine(_webHostEnvironment.WebRootPath,
                                   imageToBeDeleted.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.ProductImageRepo.RemoveRepo(imageToBeDeleted);
                _unitOfWork.SaveUnitOfWork();

                TempData["success"] = "Deleted successfully";
            }

            return RedirectToAction(nameof(Upsert), new { id = productId });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.ProductRepo.GetAllRepo(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.ProductRepo.GetRepo(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }


            _unitOfWork.ProductRepo.RemoveRepo(productToBeDeleted);
            _unitOfWork.SaveUnitOfWork();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
