using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly MovieContext _context;
        public CountryController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var country = _context.Countries.ToList();
            return View(country);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Country country)
        {
            var countryModel = new Country()
            {
                CountryId = Guid.NewGuid(),
                Name = country.Name,
                Status = country.Status,
                CreatedDate = DateTime.Now
            };
            _context.Countries.Add(countryModel);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thêm thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var countryInfo = _context.Countries.Find(id);
            return View(countryInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Country country)
        {
            var countryExist = await _context.Countries.Where(i => i.CountryId == country.CountryId).FirstOrDefaultAsync();
            if (countryExist == null)
            {
                return NotFound();
            }
            countryExist.Name = country.Name;
            countryExist.Status = country.Status;
            countryExist.CreatedDate = DateTime.Now;
            _context.Countries.Update(countryExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteItem = await _context.Countries.FindAsync(id);
            if (deleteItem == null)
            {
                return BadRequest(error: "Lỗi trong khi xóa");
            }
            _context.Countries.Remove(deleteItem);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
