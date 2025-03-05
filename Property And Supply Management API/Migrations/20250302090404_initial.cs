using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyAndSupplyManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departmentname = table.Column<string>(name: "department_name", type: "nvarchar(max)", nullable: false),
                    departmentalemail = table.Column<string>(name: "departmental_email", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemname = table.Column<string>(name: "item_name", type: "nvarchar(max)", nullable: false),
                    departmentid = table.Column<int>(name: "department_id", type: "int", nullable: false),
                    dateadded = table.Column<DateTime>(name: "date_added", type: "datetime2", nullable: false),
                    lastmodifieddate = table.Column<DateTime>(name: "last_modified_date", type: "datetime2", nullable: false),
                    maintenancedate = table.Column<DateTime>(name: "maintenance_date", type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Departments_department_id",
                        column: x => x.departmentid,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "department_name", "departmental_email" },
                values: new object[,]
                {
                    { 1, "OR", "or@gmail.com" },
                    { 2, "Cardiology", "cardiology@gmail.com" },
                    { 3, "Neurology ", "neurology@gmail.com" },
                    { 4, "Orthopedics", "orthopedics@gmail.com" },
                    { 5, "OB/GYN", "obgyn@gmail.com" },
                    { 6, "Radiology ", "radiology@gmail.com" },
                    { 7, "Oncology", "oncology@email.com" },
                    { 8, "Anesthesiology ", "ezekiel.lamoste@gmail.com" },
                    { 9, "Intensive Care Unit", "icu@gmail.com" },
                    { 10, "General Surgery ", "generalsurgery@gmail.com" },
                    { 11, "Nephrology", "nephrology@gmail.com" },
                    { 12, "Psychiatry", "psychiatry@gmail.com" },
                    { 13, "ER", "er@gmail.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_department_id",
                table: "Items",
                column: "department_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
