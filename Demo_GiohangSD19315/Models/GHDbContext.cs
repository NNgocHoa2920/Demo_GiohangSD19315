using Microsoft.EntityFrameworkCore;

namespace Demo_GiohangSD19315.Models
{
    public class GHDbContext : DbContext
    {
        //nếu add chuỗi kn luôn vào class nay thì cần contrucotr k có tham số
        //nhưng dùng cách để chuôi ở apppseting thì có hoặc k cx đc
        public GHDbContext(DbContextOptions options) : base(options)
        {
        }
        //khởi tạo db set
        //dbset đại diên cho 1 thực thể= đại diện 1 bảng
        //có bn class thì có bấy nhiêu db set
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<GHCT> GHCTs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }

    }
}
