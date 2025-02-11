using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DirectorController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public DirectorController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var directors = _context.Directors.ToList();
            return View(directors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Director director, IFormFile ProfileImage)
        {
            var directorModel = new Director();
            {
                directorModel.DirectorId = Guid.NewGuid();
                directorModel.Name = director.Name;
                directorModel.DateOfBirth = director.DateOfBirth;
                directorModel.Nationality = director.Nationality;
                directorModel.Biography = director.Biography;
                if (ProfileImage != null)
                {
                    var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/DirectorImages/", ProfileImage.FileName);//lấy đường dẫn hình
                    //lưu hình
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream); // copy vào đường dẫn stream
                        stream.Close();
                    }
                    directorModel.ProfileImage = ProfileImage.FileName;
                }
                _context.Directors.Add(directorModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var directorExist = _context.Directors.Find(id);
            return View(directorExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Director director, IFormFile ProfileImage, Guid id)
        {
            var directorExist = await _context.Directors.Where(i => i.DirectorId == director.DirectorId).FirstOrDefaultAsync();
            if (directorExist == null)
            {
                return NotFound();
            }
            directorExist.Name = director.Name;
            directorExist.DateOfBirth = director.DateOfBirth;
            directorExist.Biography = director.Biography;
            directorExist.Nationality = director.Nationality;
            if (ProfileImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/DirectorImages/", ProfileImage.FileName);//lấy đường dẫn hình
                                                                                                                                  //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                directorExist.ProfileImage = ProfileImage.FileName;
            }
            _context.Directors.Update(directorExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteDirector = _context.Directors.Find(id);
            if (deleteDirector == null)
            {
                return NotFound();
            }
            string path = this._environment.WebRootPath + "/assets/admin/images/DirectorImages/" + deleteDirector.ProfileImage;// lấy đường dẫn hình ra để xóa file hình
            _context.Directors.Remove(deleteDirector);
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
