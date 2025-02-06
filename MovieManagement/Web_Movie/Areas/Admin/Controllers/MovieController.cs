using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public MovieController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var movieList = _context.Movies.Include(m => m.Category).Include(i => i.MovieImages).ToList();
            return View(movieList);
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
        public async Task<IActionResult> Create(Movie movie, IFormFile[] MoreImage, IFormFile PrimaryImage)
        {
            var movieModel = new Movie();
            movieModel.MovieId = Guid.NewGuid();
            movieModel.MovieName = movie.MovieName;
            movieModel.Description = movie.Description;
            movieModel.Time = movie.Time;
            movieModel.Status = movie.Status;
            movieModel.CategoryId = movie.CategoryId;
            if (PrimaryImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/MovieImages/", PrimaryImage.FileName);//lấy đường dẫn hình
                                                                                                                                  //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await PrimaryImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                movieModel.PrimaryImage = PrimaryImage.FileName;
            }
            _context.Movies.Add(movieModel);
            await _context.SaveChangesAsync();
            if (MoreImage.Length > 0 || MoreImage != null)
            {
                for (int i = 0; i < MoreImage.Length; i++)
                {
                    if (MoreImage[i].Length > 0)
                    {
                        //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewsImages", file.FileName);
                        var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/MovieImages/", MoreImage[i].FileName);//lấy đường dẫn hình
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await MoreImage[i].CopyToAsync(stream); // copy vào đường dẫn stream
                            stream.Close();
                        }
                        var movieImage = new MovieImage();
                        movieImage.MovieImageId = Guid.NewGuid();
                        movieImage.MovieId = movieModel.MovieId;
                        movieImage.ImageUrl = path;
                        movieImage.ImageName = MoreImage[i].FileName;
                        _context.MoviesImages.Add(movieImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            TempData["success"] = "Thêm thành công";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            var movieExist = _context.Movies.Find(id);
            var categoryList = _context.Categories
                .Where(i => i.ParentId == Guid.Parse("0922c247-a6dc-42aa-855b-42bdfb6926e1"))
                .ToList();
            ViewBag.Categories = new SelectList(categoryList, "CategoryId", "CategoryName");
            return View(movieExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Movie movie, IFormFile[] MoreImage, IFormFile PrimaryImage)
        {
            var movieExist = _context.Movies.Find(id);
            movieExist.MovieName = movie.MovieName;
            movieExist.Description = movie.Description;
            movieExist.Time = movie.Time;
            movieExist.Status = movie.Status;
            movieExist.CategoryId = movie.CategoryId;
            if (PrimaryImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/MovieImages/", PrimaryImage.FileName);//lấy đường dẫn hình
                                                                                                                                  //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await PrimaryImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                movieExist.PrimaryImage = PrimaryImage.FileName;
            }
            _context.Movies.Update(movieExist);
            await _context.SaveChangesAsync();
            if (MoreImage.Length > 0 || MoreImage != null)
            {
                foreach (var file in MoreImage)
                {
                    if (file.Length > 0)
                    {
                        // Lấy đường dẫn lưu file
                        var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/MovieImages/", file.FileName);
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            stream.Close();
                        }

                        // Kiểm tra ảnh có tồn tại không
                        var existingImage = _context.MoviesImages.FirstOrDefault(img => img.MovieId == movieExist.MovieId);

                        if (existingImage != null)
                        {
                            // Cập nhật ảnh cũ
                            existingImage.ImageUrl = file.FileName;
                            existingImage.ImageName = file.FileName;
                            _context.MoviesImages.Update(existingImage);
                        }
                        else
                        {
                            // Nếu không có ảnh, thêm mới
                            var movieImage = new MovieImage
                            {
                                MovieImageId = Guid.NewGuid(),
                                MovieId = movieExist.MovieId,
                                ImageUrl = file.FileName,
                                ImageName = file.FileName
                            };
                            _context.MoviesImages.Add(movieImage);
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
            var ImageDelete = _context.MoviesImages.Where(i => i.MovieId == id).ToList();
            var movieDelete = _context.Movies.Find(id);
            if (movieDelete == null)
            {
                return NotFound();
            }
            // Xóa ảnh đại diện của bài viết
            string path = this._environment.WebRootPath + "/assets/admin/images/MovieImages/" + movieDelete.PrimaryImage;// lấy đường dẫn hình ra để xóa file hình            
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);//xóa hình trên lưu trên máy
            }
            // Xóa tất cả ảnh phụ của bài viết
            foreach (var image in ImageDelete)
            {
                string imagePath = Path.Combine(this._environment.WebRootPath, "assets/admin/images/MovieImages/", image.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Movies.Remove(movieDelete);
            _context.MoviesImages.RemoveRange(ImageDelete); //Xóa cả một mảng hay list
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(Guid MovieId)
        {
            var movieDetail = _context.Movies.Where(i => i.MovieId == MovieId).Include(i => i.MovieImages).Include(i => i.Category).FirstOrDefault();
            if (movieDetail == null) { return NotFound(); }
            return View(movieDetail);
        }
    }
}
