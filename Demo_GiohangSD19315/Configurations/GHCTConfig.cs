using Demo_GiohangSD19315.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GiohangSD19315.Configurations
{
    public class GHCTConfig : IEntityTypeConfiguration<GHCT>
    {
        public void Configure(EntityTypeBuilder<GHCT> builder)
        {
            //tạo khóa chính
            builder.HasKey(x => x.Id);
            //withMany: chỉ ra bảng nhiều
            //thiết lâp 1gh có n ghct
            builder.HasOne(x => x.GioHang)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.GioHangId);
            //thiết lạp 1 sp có n ghct
            builder.HasOne(x => x.SanPham)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.SanPhamId);
        }
    }
}
