using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        private readonly MovieContext _context;
        private readonly IWebHostEnvironment _environment;
        public ActorController(MovieContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var actors = _context.Actors.ToList();
            return View(actors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor, IFormFile ProfileImage)
        {
            var actorModel = new Actor();
            {
                actorModel.ActorId = Guid.NewGuid();
                actorModel.Name = actor.Name;
                actorModel.DateOfBirth = actor.DateOfBirth;
                actorModel.Nationality = actor.Nationality;
                actorModel.Biography = actor.Biography;
                if (ProfileImage != null)
                {
                    var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/ActorImages/", ProfileImage.FileName);//lấy đường dẫn hình
                    //lưu hình
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream); // copy vào đường dẫn stream
                        stream.Close();
                    }
                    actorModel.ProfileImage = ProfileImage.FileName;
                }
                _context.Actors.Add(actorModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var actorExist = _context.Actors.Find(id);
            return View(actorExist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor, IFormFile ProfileImage, Guid id)
        {
            var actorExist = await _context.Actors.Where(i => i.ActorId == actor.ActorId).FirstOrDefaultAsync();
            if (actorExist == null)
            {
                return NotFound();
            }
            actorExist.Name = actor.Name;
            actorExist.DateOfBirth = actor.DateOfBirth;
            actorExist.Biography = actor.Biography;
            actorExist.Nationality = actor.Nationality;
            if (ProfileImage != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "assets/admin/images/ActorImages/", ProfileImage.FileName);//lấy đường dẫn hình
                                                                                                                            //lưu hình
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream); // copy vào đường dẫn stream
                    stream.Close();
                }
                actorExist.ProfileImage = ProfileImage.FileName;
            }
            _context.Actors.Update(actorExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteActor = _context.Actors.Find(id);
            if (deleteActor == null)
            {
                return NotFound();
            }
            string path = this._environment.WebRootPath + "/assets/admin/images/ActorImages/" + deleteActor.ProfileImage;// lấy đường dẫn hình ra để xóa file hình
            _context.Actors.Remove(deleteActor);
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
