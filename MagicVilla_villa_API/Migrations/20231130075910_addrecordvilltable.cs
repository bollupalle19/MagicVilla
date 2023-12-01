using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_villa_API.Migrations
{
    /// <inheritdoc />
    public partial class addrecordvilltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "id", "createdate", "detalis", "imgurl", "name", "rate", "sqft", "updateddate" },
                values: new object[] { 1, new DateTime(2023, 11, 30, 13, 29, 10, 277, DateTimeKind.Local).AddTicks(4674), "Pool View Coool palce", "", "Pool view", 100000.0, 1300.0, new DateTime(2023, 11, 30, 13, 29, 10, 277, DateTimeKind.Local).AddTicks(4684) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
