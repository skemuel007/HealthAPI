using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAPI.Migrations
{
    public partial class PatientMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab466f8-4dff-4af9-a417-ea58379007b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fd677a1-2868-4304-928c-bb083f118519");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0c71924-16c0-431a-8412-38d8159f7af0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd1a7f8e-dc01-4153-8437-95f60813fd11");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "339d3c7c-1f54-4933-9d23-0efe2d7d4ffc", "93ed1b74-973d-41ae-af71-e7c11c5a52cb", "Doctor", "DOCTOR" },
                    { "cee69056-a27c-4c2d-ae3e-065df52fb4cb", "1ce92605-7b8f-4117-b331-59677c6b12d0", "Administrator", "ADMINISTRATOR" },
                    { "3a605e51-f0ec-46aa-8b23-7681b31f1502", "ea88f077-5aec-45c3-8577-e09476090d3a", "Patient", "Patient" },
                    { "05f0ddee-c104-4ba4-85b3-d0b2d7820abe", "179f62f9-5c17-45f7-b1dd-6e08fa28cc8f", "Nurse", "NURSE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05f0ddee-c104-4ba4-85b3-d0b2d7820abe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "339d3c7c-1f54-4933-9d23-0efe2d7d4ffc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a605e51-f0ec-46aa-8b23-7681b31f1502");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cee69056-a27c-4c2d-ae3e-065df52fb4cb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ab466f8-4dff-4af9-a417-ea58379007b1", "86582f58-69e4-45d8-973d-17e873701037", "Doctor", "DOCTOR" },
                    { "b0c71924-16c0-431a-8412-38d8159f7af0", "6654aac3-9364-4078-841a-b6bc80022f2d", "Administrator", "ADMINISTRATOR" },
                    { "bd1a7f8e-dc01-4153-8437-95f60813fd11", "25d7521f-2549-4c02-b833-c1588b421916", "Patient", "Patient" },
                    { "9fd677a1-2868-4304-928c-bb083f118519", "d2ef1f43-bcce-4a51-bd96-08b8efcdc1aa", "Nurse", "NURSE" }
                });
        }
    }
}
