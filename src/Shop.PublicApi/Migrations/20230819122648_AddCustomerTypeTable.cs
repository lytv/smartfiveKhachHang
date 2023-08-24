using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.PublicApi.Migrations;
    /// <inheritdoc />
public partial class AddCustomerTypeTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "CustomerTypes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerTypeCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                TenantId = table.Column<int>(type: "int", unicode: false, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CustomerTypes", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CustomerTypes");
    }
}
