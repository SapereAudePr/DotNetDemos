using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.TPT
{
    /// <inheritdoc />
    public partial class InitialTptMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Staff");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Hospital",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HospitalBuiltDate = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: false),
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
                name: "Departments",
                schema: "Staff",
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
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalSchema: "dbo",
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HospitalEmailAddress",
                schema: "dbo",
                columns: table => new
                {
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    MainEmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalEmailAddress", x => x.HospitalId);
                    table.ForeignKey(
                        name: "FK_HospitalEmailAddress_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalSchema: "dbo",
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalPhoneNumber",
                schema: "dbo",
                columns: table => new
                {
                    HospitalId = table.Column<int>(type: "int", nullable: false),
                    MainPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MainPhoneLabel = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalPhoneNumber", x => x.HospitalId);
                    table.ForeignKey(
                        name: "FK_HospitalPhoneNumber_Hospital_HospitalId",
                        column: x => x.HospitalId,
                        principalSchema: "dbo",
                        principalTable: "Hospital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentEmailAddresses",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentEmailAddresses", x => new { x.Id, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_DepartmentEmailAddresses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Staff",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPhoneNumbers",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPhoneNumbers", x => new { x.Id, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_DepartmentPhoneNumbers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Staff",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ShiftStart = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    ShiftEnd = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
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
                        name: "FK_Personnel_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Staff",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Primary table for TPT hierarchy of all staff members");

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Personnel_Id",
                        column: x => x.Id,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Janitors",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AssignedZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BiohazardCertified = table.Column<bool>(type: "bit", nullable: false),
                    SecurityClearanceLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Janitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Janitors_Personnel_Id",
                        column: x => x.Id,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsHeadNurse = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))"),
                    CertificationLevel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AssignedWard = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShiftType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nurses_Personnel_Id",
                        column: x => x.Id,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelEmailAddress",
                schema: "Staff",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelEmailAddress", x => x.PersonnelId);
                    table.ForeignKey(
                        name: "FK_PersonnelEmailAddress_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelPhone",
                schema: "Staff",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneLabel = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPhone", x => x.PersonnelId);
                    table.ForeignKey(
                        name: "FK_PersonnelPhone_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receptionists",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DeskLocation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HandlesInsuranceBilling = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receptionists_Personnel_Id",
                        column: x => x.Id,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TechnicalCategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EquipmentSpecialty = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CertificationNumber = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technicians_Personnel_Id",
                        column: x => x.Id,
                        principalSchema: "Staff",
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionistLanguages",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptionistId = table.Column<int>(type: "int", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LanguageProficiency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionistLanguages", x => new { x.Id, x.ReceptionistId });
                    table.ForeignKey(
                        name: "FK_ReceptionistLanguages_Receptionists_ReceptionistId",
                        column: x => x.ReceptionistId,
                        principalSchema: "Staff",
                        principalTable: "Receptionists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEmailAddresses_DepartmentId",
                schema: "Staff",
                table: "DepartmentEmailAddresses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPhoneNumbers_DepartmentId",
                schema: "Staff",
                table: "DepartmentPhoneNumbers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HospitalId",
                schema: "Staff",
                table: "Departments",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name_HospitalId",
                schema: "Staff",
                table: "Departments",
                columns: new[] { "Name", "HospitalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_Name",
                schema: "dbo",
                table: "Hospital",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_DepartmentId",
                schema: "Staff",
                table: "Personnel",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_Name_DepartmentId",
                schema: "Staff",
                table: "Personnel",
                columns: new[] { "Name", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistLanguages_ReceptionistId",
                schema: "Staff",
                table: "ReceptionistLanguages",
                column: "ReceptionistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentEmailAddresses",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DepartmentPhoneNumbers",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "HospitalEmailAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HospitalPhoneNumber",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Janitors",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Nurses",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "PersonnelEmailAddress",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "PersonnelPhone",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "ReceptionistLanguages",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Technicians",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Receptionists",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Personnel",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Hospital",
                schema: "dbo");
        }
    }
}
