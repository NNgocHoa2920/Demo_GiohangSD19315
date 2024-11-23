using Demo_GiohangSD19315.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using X.PagedList.Extensions;

namespace Demo_GiohangSD19315.Controllers
{
    public class SanPhamController : Controller
    {
        GHDbContext _db;
        public SanPhamController(GHDbContext db)
        {
            _db = db;
        }

        //lấy ra toàn bộ danh sách
        public IActionResult Index(string name,int page=4, int pageSize=2)
        {
            //lấy giá trị session có tên là userName
            var sesiondata = HttpContext.Session.GetString("cun");
            if (sesiondata == null) // chưa đăng nhập
            {
                TempData["mess"] = "đăng nhập đi bạn ơi";
                return RedirectToAction("Login","Account");
            }
            else
            {
                ViewData["mess1"] = $"Mời {sesiondata} xem sản phẩm";
                //var data = _db.SanPhams.ToList();
                //return View(data);
            }
            //lấ ra ds sanphaamm
            var data = _db.SanPhams.ToPagedList(page, pageSize);

            //check xem ô tìm kiếm có đc không
            if(string.IsNullOrEmpty(name))
            {
                return View(data); // nếu k nhập thì hiện thị toàn bộ ds sản phẩm
            }
            else
            {
                var search = _db.SanPhams
                    .Where(x=>x.SanPhamName.ToUpper().Contains(name.ToUpper()))
                    .ToList();
                //lưu số lượng tìm kiếm vào view data và viewbag
                ViewData["count"] = search.Count;
                ViewBag.Count = search.Count;
                if (search.Count==0)
                {
                    return View(data);
                }
                else
                {
                    return View(search);
                }
            }
        }

        //thêm đối tượng
        //hiên thị tạo form crate
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp)
        {
            _db.SanPhams.Add(sp);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        //tạo ra view form edit có chứa dữ liệu cần edit
        public IActionResult Edit(Guid id)
        {
            //tìm đối tượng cần sửa
            var sp = _db.SanPhams.Find(id);
            return View(sp);
        }
        [HttpPost]
        public IActionResult Edit(SanPham sp)
        {
            _db.SanPhams.Update(sp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //xem chi tiết sản phẩm
        public IActionResult Details(Guid id)
        {
            //tìm đối tượng cần sửa
            var sp = _db.SanPhams.Find(id);
            return View(sp);
        }

        //thêm sản phẩm vào giỏ hàng 
        public IActionResult AddToCart(Guid id, int soLuong)//id : là id sản phẩm đc thêm vào giỏ hang
        {
            //lấy ra usernam tương ứng vs giỏ hàng
            //b1: lấy ra username mà vừa ms đăng nhập
            //mình set cho username = cun
            var user = HttpContext.Session.GetString("cun");
            if (user == null)
            {
                return Content("chưa đăng nhập hoặc hết hạn");
            }
            //b2: lấy thông tin của acc
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            //lấy giỏ hàng tương ứng vs tài khoản đc đăng nhạp
            var gioHang = _db.GioHangs.FirstOrDefault(x => x.AccountId == acc.Id);
            if (gioHang == null)
            {
                return Content("Chưa có giỏ hàng");
            }

            //lấy toàn bộ sản phẩm trong giỏ hàng chi tiết của acc
            var accCart = _db.GHCTs.Where(x => x.GioHangId == gioHang.Id).ToList();


            //DUYỆT GHCT ĐỂ XỬ LÍ CỘNG DỒN GIỎ HÀNG
            bool check = false;
            Guid idGHCT = Guid.NewGuid();
            foreach (var item in accCart)
            {
                //check sp mới add trfng vs sp đã có trong gh
                if (item.SanPhamId == id)
                {
                    check = true;
                    idGHCT = item.Id; //lấy ra id của ghct để lát nưa update
                    break;
                }
            }
            //nếu sp chưa đc chọn
            if (!check)
            {
                //TẠO RA GHCT TƯNG ỨNG VS SP ĐS
                GHCT ghct = new GHCT()
                {
                    SanPhamId = id,
                    GioHangId = gioHang.Id,
                    SoLuong = soLuong
                };
                _db.GHCTs.Add(ghct);
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                var ghctUpdte = _db.GHCTs.FirstOrDefault(x => x.Id == idGHCT);
                ghctUpdte.SoLuong = ghctUpdte.SoLuong + soLuong;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }


        }
    }
}
    

