using Demo_GiohangSD19315.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Demo_GiohangSD19315.Controllers
{
    public class AccountController : Controller
    {
        //goị class đại diện cho csdl ở đayy
        
        private readonly GHDbContext _db;

        //Tiêm DI: tiêm csdl vào trong controller
        public AccountController(GHDbContext db)
        {
            _db = db;
        }

        //dùng để hiện thị toàn bộ dữ liệu của account
        public IActionResult Index()
        {
            //láy giá trị session đã lưu sau khi đăng nhập
            //key "cun" = userName
            var sessionData = HttpContext.Session.GetString("cun");
            if(sessionData == null)
            {
                return Content("Đăng nhập đi bạn ê");
               
            }
            else
            {
                ViewData["mess"] = $"Chào mừng {sessionData} đến vs bình nguyên vô tận";

            }
            //lấy toàn bộ user
            var accountData = _db.Accounts.ToList();
            return View(accountData);
        }

        //Đăng kí
        //tạo ra form đăng kí
        public IActionResult Dangky()
        {
            return View();
        }
        //xử lí logic của đăng kí
        [HttpPost]
        public IActionResult DangKy(Account account)
        {
            try
            {
                //tạo 1 account
                _db.Accounts.Add(account);
                //khi tạo 1 account đồng thời sẽ tạo 1 giỏ hàng
                GioHang giohang = new GioHang()
                {
                    UserName = account.UserName,
                    AccountId = account.Id
                };
                //ađ giỏ hàng
                _db.GioHangs.Add(giohang);
                _db.SaveChanges();
                TempData["Status"] = "Chúc bạn đã tạo tài khoản thành công";
                return RedirectToAction("Login");
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }

        //chức năng đăng nhập = login
        public IActionResult Login()
        {
            return View();
        }
        //xử lí logic của login
        [HttpPost]
        public IActionResult Login(string userName , string passWord)
        {
            //check xem userName và password có nhập hay k
            if(userName == null || passWord == null)
            {
                //khi k nhập thì view vẫn giữ nguyên là view login
                return View(); 
            }
            //tìm kiếm xem thông tin userName và pass có tồn tại trong csdl
            var acc = _db.Accounts.ToList()
                .FirstOrDefault(x => x.UserName == userName && x.Password == passWord);
            if(acc == null)
            {
                return Content("Tài khoản hoặc mật khẩu chưa chính xác");
            }
            else
            {
                //lưu dữ liệu login vào session vs key là cun
                HttpContext.Session.SetString("cun", userName);
                return RedirectToAction("Index","SanPham");
            }
        }
    }
}
