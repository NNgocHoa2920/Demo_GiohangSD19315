using Demo_GiohangSD19315.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_GiohangSD19315.Configurations
{
    public class GioHangConfig : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            builder.HasKey(x => x.Id);
            //thiết lập mqh 1-1 trong bảng gh
            builder.HasOne(x => x.Account)
                .WithOne(x => x.GioHang)
                .HasForeignKey<GioHang>(x => x.AccountId);
        }
    }
}
