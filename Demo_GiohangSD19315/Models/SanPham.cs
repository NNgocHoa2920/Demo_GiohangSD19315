namespace Demo_GiohangSD19315.Models
{
    public class SanPham
    {
        public Guid SanPhamId { get; set; }
        public string SanPhamName { get; set; }
        public decimal Price {  get; set; } 
        //IColection, Ilist, List, colelection
        //thể hiện 1 sản phẩm có nhiều ghct
        //để sử dụng làm navigatiion để trỏ đến khi cần
        public List<GHCT> GHCTs { get; set; }
    }
}
