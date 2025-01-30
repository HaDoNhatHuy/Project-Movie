using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdsController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public AdsController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var ads = _context.Advs.ToList();
            return View(ads);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ads ads, IFormFile AdsImage)
        {
            var adsModel = new Ads();
            {
                adsModel.AdsId = Guid.NewGuid();
                adsModel.AdsName = ads.AdsName;
                adsModel.AdsUrl = ads.AdsUrl;
                adsModel.Status = ads.Status;
                adsModel.CreatedDate = DateTime.Now;
                if (AdsImage != null)
                {
                    var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/AdsImages/", AdsImage.FileName);//lấy đường dẫn hình
                    //lưu hình
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await AdsImage.CopyToAsync(stream); // copy vào đường dẫn stream
                        stream.Close();
                    }
                    adsModel.AdsImage = AdsImage.FileName;
                }
                _context.Advs.Add(adsModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var adsExist = _context.Advs.Find(id);
            return View(adsExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Ads ads, IFormFile AdsImage, Guid id)
        {
            var adsExist = await _context.Advs.Where(i => i.AdsId == ads.AdsId).FirstOrDefaultAsync();
            if (adsExist == null)
            {
                return NotFound();
            }
            adsExist.AdsName = ads.AdsName;
            adsExist.AdsUrl = ads.AdsUrl;
            adsExist.Status = ads.Status;
            adsExist.CreatedDate = DateTime.Now;
            if (AdsImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/AdsImages/", AdsImage.FileName);//lấy đường dẫn hình
                                                                                                                           //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await AdsImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                adsExist.AdsImage = AdsImage.FileName;
            }
            _context.Advs.Update(adsExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteAds = _context.Advs.Find(id);
            if (deleteAds == null)
            {
                return NotFound();
            }
            string path = this._environment.WebRootPath + "/assets/admin/images/AdsImages/" + deleteAds.AdsImage;// lấy đường dẫn hình ra để xóa file hình
            _context.Advs.Remove(deleteAds);
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
