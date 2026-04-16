using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.TPH
{
    /// <inheritdoc />
    public partial class InitialTphMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MainPhoneLabel = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    MainEmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BuiltDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospital", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentEmailAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentEmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentEmailAddresses_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentPhoneNumbers_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber_Label = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EmailAddress_Value = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    PersonnelType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ShiftEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AssignedZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BiohazardCertified = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    SecurityClearanceLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsHeadNurse = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CertificationLevel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AssignedWard = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ShiftType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DeskLocation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    HandlesInsuranceBilling = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    TechnicalCategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EquipmentSpecialty = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CertificationNumber = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnel_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionistLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Proficiency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionistLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptionistLanguages_Personnel_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_HospitalId",
                table: "Department",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name_HospitalId",
                table: "Department",
                columns: new[] { "Name", "HospitalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEmailAddresses_DepartmentId",
                table: "DepartmentEmailAddresses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEmailAddresses_DepartmentId_Value",
                table: "DepartmentEmailAddresses",
                columns: new[] { "DepartmentId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPhoneNumbers_DepartmentId",
                table: "DepartmentPhoneNumbers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPhoneNumbers_DepartmentId_Number_Label",
                table: "DepartmentPhoneNumbers",
                columns: new[] { "DepartmentId", "Number", "Label" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Name",
                table: "Hospital",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_DepartmentId",
                table: "Personnel",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistLanguages_ReceptionistId",
                table: "ReceptionistLanguages",
                column: "ReceptionistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentEmailAddresses");

            migrationBuilder.DropTable(
                name: "DepartmentPhoneNumbers");

            migrationBuilder.DropTable(
                name: "ReceptionistLanguages");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Hospital");
        }
    }
}
