using Microsoft.AspNetCore.Mvc;
using Buoi3.Models;
using System.Linq.Dynamic.Core;

namespace Buoi3.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> products = new List<Product>()
            {
                new Product(){ProductId=1, ProductName="Laptop", ProductPrice=999.99m, Description="", CategoryId=1},
                new Product(){ProductId=2, ProductName="Smartphone", ProductPrice=499.99m, Description="", CategoryId=1},
                new Product(){ProductId=3, ProductName="Novel", ProductPrice=19.99m, Description="", CategoryId=2},
                new Product(){ProductId=4, ProductName="Textbook", ProductPrice=59.99m, Description="", CategoryId=2},
                new Product(){ProductId=5, ProductName="Tablet", ProductPrice=299.99m, Description="", CategoryId=1},
                new Product(){ProductId=6, ProductName="Headphones", ProductPrice=79.99m, Description="", CategoryId=1},
                new Product(){ProductId=7, ProductName="Dictionary", ProductPrice=35.99m, Description="", CategoryId=2},
                new Product(){ProductId=8, ProductName="Comic Book", ProductPrice=12.99m, Description="", CategoryId=2},
                new Product(){ProductId=9, ProductName="Keyboard", ProductPrice=49.99m, Description="", CategoryId=1},
                new Product(){ProductId=10, ProductName="Notebook", ProductPrice=9.99m, Description="", CategoryId=2},
            };

        // Lấy dữ liệu hiển thị: Join Product + Category -> ProductViewModel (như video)
        private List<ProductViewModel> GetViewModels()
        {
            return products.Join(CategoryController.categories,
                p => p.CategoryId,
                c => c.CategoryId,
                (p, c) => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    CategoryName = c.CategoryName
                }).ToList();
        }

        // Tìm kiếm: như Service.Get(term) trong bài, tìm trong ProductName, CategoryName, ProductPrice, ProductId
        private List<ProductViewModel> SearchData(List<ProductViewModel> data, string? term)
        {
            if (string.IsNullOrWhiteSpace(term)) return data;
            var s = term.Trim().ToLower();
            return data.Where(v =>
                v.ProductName.ToLower().Contains(s) ||
                v.CategoryName.ToLower().Contains(s) ||
                v.ProductPrice.ToString().Contains(s) ||
                v.ProductId.ToString() == s
            ).ToList();
        }

        // Tìm kiếm theo bài: action Search riêng, chuyển về Index để giữ phân trang + sắp xếp
        public IActionResult Search(string term)
        {
            return RedirectToAction("Index", new { term });
        }

        // Phân trang + sắp xếp bằng chuỗi: như Service.Paging trong bài
        private (ProductViewModel[] items, int pages, int page, int total) Paging(
            List<ProductViewModel> data, int page, string orderBy = "ProductName", bool dsc = false)
        {
            int size = 5;
            int total = data.Count;
            int pages = (int)Math.Ceiling((double)total / size);
            if (pages == 0) pages = 1;
            if (page < 1) page = 1;
            if (page > pages) page = pages;

            var allowed = new[] { "ProductId", "ProductName", "ProductPrice", "CategoryName" };
            if (!allowed.Contains(orderBy)) orderBy = "ProductName";

            var items = data.AsQueryable()
                .OrderBy($"{orderBy} {(dsc ? "descending" : "")}")
                .Skip((page - 1) * size)
                .Take(size)
                .ToArray();
            return (items, pages, page, total);
        }

        // GET: Product/Index?page=1&orderBy=ProductName&dsc=false&term=
        public IActionResult Index(int page = 1, string orderBy = "ProductName", bool dsc = false, string? term = null)
        {
            var data = SearchData(GetViewModels(), term);
            var model = Paging(data, page, orderBy, dsc);

            ViewData["Pages"] = model.pages;
            ViewData["Page"] = model.page;
            ViewData["Total"] = model.total;
            ViewData["Term"] = term ?? "";
            ViewData["OrderBy"] = orderBy;
            ViewData["Dsc"] = dsc;

            // Tiểu xảo như trong bài: để header biết lần bấm sau là tăng hay giảm
            ViewData["ProductId"] = false;
            ViewData["ProductName"] = false;
            ViewData["ProductPrice"] = false;
            ViewData["CategoryName"] = false;
            ViewData[orderBy] = !dsc;

            return View(model.items);
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var category = CategoryController.categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            var model = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                CategoryName = category?.CategoryName ?? string.Empty
            };
            return View(model);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            product.ProductId = products.Any() ? products.Max(p => p.ProductId) + 1 : 1;
            products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            var existing = products.FirstOrDefault(p => p.ProductId == id);
            if (existing == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                product.ProductId = id;
                return View(product);
            }

            existing.ProductName = product.ProductName;
            existing.ProductPrice = product.ProductPrice;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
