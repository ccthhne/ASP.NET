using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Buoi3.Models;
namespace Buoi3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        //Gia lap database
        public static List<Category> categories = new List<Category>()
            {
                new Category(){CategoryId=1, CategoryName="Electronics"},
                new Category(){CategoryId=2, CategoryName="Books"},
                new Category(){CategoryId=3, CategoryName="Tablet"},
            };

        // GET: CategoryController
        [HttpGet]
        public ActionResult Index()
        {
            return View(categories);
        }

        // GET: CategoryController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Details action called with ID: {Id}", id);
            var category = categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CategoryId = categories.Any() ? categories.Max(c => c.CategoryId) + 1 : 1;
                categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var EditCategory = categories.FirstOrDefault(c => c.CategoryId == id);
            if (EditCategory == null)
            {
                return NotFound();
            }
            return View(EditCategory);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            var existingCategory = categories.FirstOrDefault(c => c.CategoryId == id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                category.CategoryId = id;
                return View(category);
            }

            existingCategory.CategoryName = category.CategoryName;

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            categories.Remove(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
