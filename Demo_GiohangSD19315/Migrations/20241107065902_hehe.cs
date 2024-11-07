using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_GiohangSD19315.Migrations
{
    /// <inheritdoc />
    public partial class hehe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    SanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SanPhamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.SanPhamId);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GHCTs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SanPhamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GioHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GHCTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GHCTs_GioHangs_GioHangId",
                        column: x => x.GioHangId,
                        principalTable: "GioHangs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GHCTs_SanPhams_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GHCTs_GioHangId",
                table: "GHCTs",
                column: "GioHangId");

            migrationBuilder.CreateIndex(
                name: "IX_GHCTs_SanPhamId",
                table: "GHCTs",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_AccountId",
                table: "GioHangs",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GHCTs");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
