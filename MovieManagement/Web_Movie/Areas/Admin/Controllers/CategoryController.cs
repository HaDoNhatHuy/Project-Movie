using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly MovieContext _context;
        public CategoryController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index(Guid? parentId)
        {
            var categories = parentId == null
                ? _context.Categories.Where(c => c.ParentId == null).ToList()  // Danh mục gốc
                : _context.Categories.Where(c => c.ParentId == parentId).ToList();  // Danh mục con của parentId

            ViewBag.ParentId = parentId;
            // Trả về danh sách categories cho view
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetChildCategory(Guid parentId)
        {
            var subCategories = await _context.Categories
                .Where(c => c.ParentId == parentId)
                .Select(c => new
                {
                    c.CategoryId,
                    c.CategoryName,
                    c.CreatedDate,
                    c.Status
                }).ToListAsync();

            return Json(subCategories);
        }
        public IActionResult Create(Guid? parentId)
        {
            if (parentId.HasValue)
            {
                ViewData["ParentId"] = parentId.Value;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid? parentId, Category category)
        {
            var categoryModel = new Category();
            categoryModel.CategoryId = Guid.NewGuid();
            categoryModel.CategoryName = category.CategoryName;
            categoryModel.Status = category.Status;
            categoryModel.CreatedDate = DateTime.Now;
            if (parentId == Guid.Empty)
                categoryModel.ParentId = null;
            categoryModel.ParentId = parentId;
            _context.Categories.Add(categoryModel);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thêm thành công";
            return Redirect(Request.Headers["Referer"]); //quay lại trang ngay trước đó
        }
        public IActionResult Edit(Guid? parentId, Guid id)
        {
            var categoryExist = _context.Categories.Find(id);
            if (parentId.HasValue)
            {
                ViewData["ParentId"] = parentId.Value;
            }
            return View(categoryExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid? parentId, Guid id, Category category)
        {
            var categoryExist = _context.Categories.Find(id);
            if (categoryExist == null)
            {
                return NotFound();
            }
            categoryExist.CategoryName = category.CategoryName;
            categoryExist.Status = category.Status;
            categoryExist.CreatedDate = DateTime.Now;
            if (parentId == Guid.Empty)
                categoryExist.ParentId = null;
            categoryExist.ParentId = parentId;
            _context.Categories.Update(categoryExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return Redirect(Request.Headers["Referer"]); //quay lại trang ngay trước đó
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryExist=_context.Categories.Find(id);
            if(categoryExist == null)
            {
                return NotFound();
            }    
            _context.Categories.Remove(categoryExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return Redirect(Request.Headers["Referer"]); //quay lại trang ngay trước đó
        }
    }
}
