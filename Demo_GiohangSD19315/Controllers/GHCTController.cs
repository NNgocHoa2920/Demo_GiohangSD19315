using Demo_GiohangSD19315.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_GiohangSD19315.Controllers
{
    public class GHCTController : Controller
    {
        GHDbContext _db;
        public GHCTController(GHDbContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            var acc = HttpContext.Session.GetString("cun");
            //lsy thông tin account tương ứng vs phiên đn
            var getUser = _db.Accounts.FirstOrDefault(x=>x.UserName== acc);
            //láy gh tương ứng vs acc
            var gh = _db.GioHangs.FirstOrDefault(x => x.AccountId == getUser.Id);
            //lấy toàn bộ dữ liệu của ghct
            var data = _db.GHCTs.Where(x => x.GioHangId == gh.Id).ToList();
            return View(data);
        }
    }
}
