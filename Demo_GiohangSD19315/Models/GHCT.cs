namespace Demo_GiohangSD19315.Models
{
    public class GHCT
    {
        public Guid Id { get; set; }
        public Guid? SanPhamId { get; set; } // khóa ngoại
        public Guid? GioHangId { get; set; }
        public int SoLuong {  get; set; }
        public GioHang? GioHang { get; set; }
        public SanPham? SanPham { get; set; }

    }
}
