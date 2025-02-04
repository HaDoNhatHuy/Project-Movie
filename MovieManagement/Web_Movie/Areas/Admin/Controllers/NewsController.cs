using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public NewsController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var news = _context.News.Include(i => i.Category).Include(i => i.NewsImages).ToList();
            return View(news);
        }
        public IActionResult Create()
        {
            var categoryList = _context.Categories
                .Where(i => i.ParentId == Guid.Parse("0922c247-a6dc-42aa-855b-42bdfb6926e1"))
                .ToList();
            ViewBag.Categories = new SelectList(categoryList, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(News news, IFormFile[] MoreImage, IFormFile PrimaryImage)
        {
            var newsModel = new News();
            newsModel.NewsId = Guid.NewGuid();
            newsModel.NewsTitle = news.NewsTitle;
            newsModel.Description = news.Description;
            newsModel.Status = news.Status;
            newsModel.CategoryId = news.CategoryId;
            if (PrimaryImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/NewsImages/", PrimaryImage.FileName);//lấy đường dẫn hình
                                                                                                                                 //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await PrimaryImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                newsModel.PrimaryImage = PrimaryImage.FileName;
            }
            _context.News.Add(newsModel);
            await _context.SaveChangesAsync();
            if (MoreImage.Length > 0 || MoreImage != null)
            {
                for (int i = 0; i < MoreImage.Length; i++)
                {
                    if (MoreImage[i].Length > 0)
                    {
                        //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewsImages", file.FileName);
                        var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/NewsImages/", MoreImage[i].FileName);//lấy đường dẫn hình
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await MoreImage[i].CopyToAsync(stream); // copy vào đường dẫn stream
                            stream.Close();
                        }
                        var newsImage = new NewsImage();
                        newsImage.NewsImageId = Guid.NewGuid();
                        newsImage.NewsId = newsModel.NewsId;
                        newsImage.ImageUrl = path;
                        newsImage.ImageName = MoreImage[i].FileName;
                        _context.NewsImages.Add(newsImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            TempData["success"] = "Thêm thành công";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            var newsExist = _context.News.Find(id);
            var categoryList = _context.Categories
                .Where(i => i.ParentId == Guid.Parse("0922c247-a6dc-42aa-855b-42bdfb6926e1"))
                .ToList();
            ViewBag.Categories = new SelectList(categoryList, "CategoryId", "CategoryName");
            return View(newsExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, News news, IFormFile[] MoreImage, IFormFile PrimaryImage)
        {
            var newsExist = _context.News.Find(id);
            newsExist.NewsTitle = news.NewsTitle;
            newsExist.Description = news.Description;
            newsExist.Status = news.Status;
            newsExist.CategoryId = news.CategoryId;
            if (PrimaryImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/NewsImages/", PrimaryImage.FileName);//lấy đường dẫn hình
                                                                                                                                 //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await PrimaryImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                newsExist.PrimaryImage = PrimaryImage.FileName;
            }
            _context.News.Update(newsExist);
            await _context.SaveChangesAsync();
            if (MoreImage.Length > 0 || MoreImage != null)
            {
                foreach (var file in MoreImage)
                {
                    if (file.Length > 0)
                    {
                        // Lấy đường dẫn lưu file
                        var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/NewsImages/", file.FileName);
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            stream.Close();
                        }

                        // Kiểm tra ảnh có tồn tại không
                        var existingImage = _context.NewsImages.FirstOrDefault(img => img.NewsId == newsExist.NewsId);

                        if (existingImage != null)
                        {
                            // Cập nhật ảnh cũ
                            existingImage.ImageUrl = file.FileName;
                            existingImage.ImageName = file.FileName;
                            _context.NewsImages.Update(existingImage);
                        }
                        else
                        {
                            // Nếu không có ảnh, thêm mới
                            var newsImage = new NewsImage
                            {
                                NewsImageId = Guid.NewGuid(),
                                NewsId = newsExist.NewsId,
                                ImageUrl = file.FileName,
                                ImageName = file.FileName
                            };
                            _context.NewsImages.Add(newsImage);
                        }
                    }
                }
            }
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ImageDelete = _context.NewsImages.Where(i => i.NewsId == id).ToList();
            var newsDelete = _context.News.Find(id);
            if (newsDelete == null)
            {
                return NotFound();
            }
            // Xóa ảnh đại diện của bài viết
            string path = this._environment.WebRootPath + "/assets/admin/images/AboutImages/" + newsDelete.PrimaryImage;// lấy đường dẫn hình ra để xóa file hình            
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);//xóa hình trên lưu trên máy
            }
            // Xóa tất cả ảnh phụ của bài viết
            foreach (var image in ImageDelete)
            {
                string imagePath = Path.Combine(this._environment.WebRootPath, "assets/admin/images/NewsImages/", image.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.News.Remove(newsDelete);
            _context.NewsImages.RemoveRange(ImageDelete); //Xóa cả một mảng hay list
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
