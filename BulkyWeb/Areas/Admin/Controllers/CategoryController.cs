
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        // ApplicationDbContext db = new ApplicationDbContext();

        //private readonly ApplicationDbContext _db;
        //private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;


        //public CategoryController(ICategoryRepository db)
        //{
        //    _categoryRepo = db;
        //}

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public IActionResult Index()
        //{
        //    List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
        //    return View(objCategoryList);
        //}
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        //byte default get action method
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cannot match the name");
            }

            //{
            //    ModelState.AddModelError("", "test is an invalid name");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            //Category categoryFromDB = _db.Categories.Find(id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id ==id);
            //Category? categoryFromDB2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");

            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            //Category categoryFromDB = _db.Categories.Find(id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id ==id);
            //Category? categoryFromDB2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            //Category? obj = _db.Categories.Find(id);
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("index");
        }
    }
}
