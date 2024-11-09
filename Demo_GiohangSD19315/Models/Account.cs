using System.ComponentModel.DataAnnotations;

namespace Demo_GiohangSD19315.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        //[Required]
        //[StringLength(450,MinimumLength =10, ErrorMessage ="Độ dài phải từ 10-450")]
        public string Name { get; set; }
        public string UserName {  get; set; }
        public string Password { get; set; }
        public DateTime NgaySinh { get; set; }
        //[RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$",
        //        ErrorMessage = "số điên thoại phải đúng format xxx-")]
        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$",
          ErrorMessage = "Số điện thoại phải đúng format và có 10 chữ số xxx-xxx-xxxx")]
        public string SDT {  get; set; }
        //khai báo 1 đối tượng giỏ hàng vào trong user
        public GioHang? GioHang { get; set; }// thiết lập mối liên kết
        //đóng va trò khóa ngoại
        //? : có hoặc k cx đc
    }
}
