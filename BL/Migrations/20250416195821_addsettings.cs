using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class addsettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbItemTypes",
                table: "TbItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems");

            migrationBuilder.AlterColumn<int>(
                name: "RamSize",
                table: "TbItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OsId",
                table: "TbItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeId",
                table: "TbItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TbSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwitterLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstgramLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YoutubeLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddlePanner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastPanner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSettings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbItemTypes",
                table: "TbItems",
                column: "ItemTypeId",
                principalTable: "TbItemTypes",
                principalColumn: "ItemTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems",
                column: "OsId",
                principalTable: "TbOs",
                principalColumn: "OsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbItemTypes",
                table: "TbItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems");

            migrationBuilder.DropTable(
                name: "TbSettings");

            migrationBuilder.AlterColumn<int>(
                name: "RamSize",
                table: "TbItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OsId",
                table: "TbItems",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeId",
                table: "TbItems",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbItemTypes",
                table: "TbItems",
                column: "ItemTypeId",
                principalTable: "TbItemTypes",
                principalColumn: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbOs",
                table: "TbItems",
                column: "OsId",
                principalTable: "TbOs",
                principalColumn: "OsId");
        }
    }
}
