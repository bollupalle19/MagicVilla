using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_villa_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdate", "imgurl", "updateddate" },
                values: new object[] { new DateTime(2023, 11, 30, 13, 30, 48, 43, DateTimeKind.Local).AddTicks(2046), "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fluxury-villa&psig=AOvVaw3-aHod71srN4UaKelBLHRF&ust=1701417603723000&source=images&cd=vfe&ved=0CBIQjRxqFwoTCMiL3tGg64IDFQAAAAAdAAAAABAE", new DateTime(2023, 11, 30, 13, 30, 48, 43, DateTimeKind.Local).AddTicks(2061) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdate", "imgurl", "updateddate" },
                values: new object[] { new DateTime(2023, 11, 30, 13, 29, 10, 277, DateTimeKind.Local).AddTicks(4674), "", new DateTime(2023, 11, 30, 13, 29, 10, 277, DateTimeKind.Local).AddTicks(4684) });
        }
    }
}
