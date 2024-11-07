namespace Demo_GiohangSD19315.Models
{
    public class GioHang
    {
        public Guid Id { get; set; }
        public string UserName {  get; set; }

        public Guid SanPhamId { get; set; }

        //khởi tạo đối tượng account vào giỏ hang để đảm bảo mqh 1-1
        public Account? Account { get; set; }  
        public Guid? AccountId { get; set; }// khóa ngoại
        //thiết lập 1 giỏ hàng có nhiều ghct
        public List<GHCT> GHCTs { get; set; }
    }
}
