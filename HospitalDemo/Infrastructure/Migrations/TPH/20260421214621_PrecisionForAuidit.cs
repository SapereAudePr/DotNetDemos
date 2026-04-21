using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.TPH
{
    /// <inheritdoc />
    public partial class PrecisionForAuidit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                schema: "Staff",
                table: "Personnel",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Staff",
                table: "Personnel",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                schema: "dbo",
                table: "Hospital",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "dbo",
                table: "Hospital",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                schema: "Staff",
                table: "Department",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Staff",
                table: "Department",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                schema: "Staff",
                table: "Personnel",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Staff",
                table: "Personnel",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                schema: "dbo",
                table: "Hospital",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "dbo",
                table: "Hospital",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdateDate",
                schema: "Staff",
                table: "Department",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                schema: "Staff",
                table: "Department",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
