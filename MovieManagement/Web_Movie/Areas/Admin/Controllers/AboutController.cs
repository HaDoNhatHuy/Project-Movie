using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public AboutController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var about = _context.Abouts.ToList();
            return View(about);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(About about, IFormFile Image)
        {
            var aboutModel = new About();
            {
                aboutModel.AboutId = Guid.NewGuid();
                aboutModel.Name = about.Name;
                aboutModel.Description = about.Description;
                aboutModel.Status = about.Status;
                aboutModel.CreatedDate = DateTime.Now;
                if (Image != null)
                {
                    var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/AboutImages/", Image.FileName);//lấy đường dẫn hình
                    //lưu hình
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream); // copy vào đường dẫn stream
                        stream.Close();
                    }
                    aboutModel.Image = Image.FileName;
                }
                _context.Abouts.Add(aboutModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm giới thiệu thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var aboutInfo = _context.Abouts.Find(id);
            return View(aboutInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(About about, IFormFile Image, Guid id)
        {
            var aboutExist = await _context.Abouts.Where(i => i.AboutId == about.AboutId).FirstOrDefaultAsync();
            if (aboutExist == null)
            {
                return NotFound();
            }
            aboutExist.Name = about.Name;
            aboutExist.Description = about.Description;
            aboutExist.Status = about.Status;
            if (Image != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/AboutImages/", Image.FileName);//lấy đường dẫn hình
                                                                                                                           //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                aboutExist.Image = Image.FileName;
            }
            _context.Abouts.Update(aboutExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteAbout = _context.Abouts.Find(id);
            if (deleteAbout == null)
            {
                return NotFound();
            }
            string path = this._environment.WebRootPath + "/assets/admin/images/AboutImages/" + deleteAbout.Image;// lấy đường dẫn hình ra để xóa file hình
            _context.Abouts.Remove(deleteAbout);
            await _context.SaveChangesAsync();
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);//xóa hình trên lưu trên máy
            }
            TempData["success"] = "Xóa giới thiệu thành công";
            return RedirectToAction("Index");
        }
    }
}
