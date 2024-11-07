using Demo_GiohangSD19315.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GiohangSD19315.Configurations
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
           //thiết lâp khóa chính
           builder.HasKey(x => x.Id);
            //config 1 số các thuộc tính
            //thiết lập thuộc tính Passwrok có tên la "MatKhau" ở trong db
            //và có kiểu dữ liệu là navarchar(256)
            builder.Property(x => x.Password)
                .HasColumnName("MatKhau")
                .HasColumnType("nvarchar(256)");
            //name: nvarchar(256)
            builder.Property(x => x.Name)
                .IsUnicode(true)
                .IsFixedLength(true)
                .HasMaxLength(256);
            //thiết lập mqh 1-1 vs giỏ hàng
            //hasone: thể hiện bảng 1
            //withone: 1 với bnrg
            builder.HasOne(x => x.GioHang)
                .WithOne(x => x.Account)
                .HasForeignKey<GioHang>(x => x.AccountId);
                


        }
    }
}
