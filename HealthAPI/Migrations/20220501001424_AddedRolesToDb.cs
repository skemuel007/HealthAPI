using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthAPI.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
