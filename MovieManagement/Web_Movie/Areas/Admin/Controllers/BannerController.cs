using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public BannerController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var banner = _context.Banners.ToList();
            return View(banner);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Banner banner, IFormFile Image)
        {
            var bannerModel = new Banner();
            {
                bannerModel.BannerId = Guid.NewGuid();
                bannerModel.Name = banner.Name;
                bannerModel.Url = banner.Url;
                bannerModel.Status = banner.Status;
                bannerModel.Description = banner.Description;
                bannerModel.CreatedDate = DateTime.Now;
                if (Image != null)
                {
                    var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/BannerImages/", Image.FileName);//lấy đường dẫn hình
                    //lưu hình
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream); // copy vào đường dẫn stream
                        stream.Close();
                    }
                    bannerModel.Image = Image.FileName;
                }
                _context.Banners.Add(bannerModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var bannerExist = _context.Banners.Find(id);
            return View(bannerExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Banner banner, IFormFile Image, Guid id)
        {
            var bannerExist = await _context.Banners.Where(i => i.BannerId == banner.BannerId).FirstOrDefaultAsync();
            if (bannerExist == null)
            {
                return NotFound();
            }
            bannerExist.Name = banner.Name;
            bannerExist.Url = banner.Url;
            bannerExist.Status = banner.Status;
            bannerExist.Description = banner.Description;
            bannerExist.CreatedDate = DateTime.Now;
            if (Image != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/BannerImages/", Image.FileName);//lấy đường dẫn hình
                                                                                                                            //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                bannerExist.Image = Image.FileName;
            }
            _context.Banners.Update(bannerExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteBanner = _context.Banners.Find(id);
            if (deleteBanner == null)
            {
                return NotFound();
            }
            string path = this._environment.WebRootPath + "/assets/admin/images/BannerImages/" + deleteBanner.Image;// lấy đường dẫn hình ra để xóa file hình
            _context.Banners.Remove(deleteBanner);
            await _context.SaveChangesAsync();
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);//xóa hình trên lưu trên máy
            }
            TempData["success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
