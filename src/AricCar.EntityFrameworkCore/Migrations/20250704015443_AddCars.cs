using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AricCar.Migrations
{
    /// <inheritdoc />
    public partial class AddCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_AppRegions_DistrictCode",
                table: "AppRegions",
                column: "DistrictCode");

            migrationBuilder.CreateTable(
                name: "AppCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DistrctCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCars_AppRegions_DistrctCode",
                        column: x => x.DistrctCode,
                        principalTable: "AppRegions",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCarImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CarId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExtraProperties = table.Column<string>(type: "TEXT", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCarImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCarImages_AppCars_CarId",
                        column: x => x.CarId,
                        principalTable: "AppCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCarImages_CarId",
                table: "AppCarImages",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCars_DistrctCode",
                table: "AppCars",
                column: "DistrctCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCarImages");

            migrationBuilder.DropTable(
                name: "AppCars");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AppRegions_DistrictCode",
                table: "AppRegions");
        }
    }
}
