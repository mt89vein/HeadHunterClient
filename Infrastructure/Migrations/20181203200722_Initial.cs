using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(type: "DECIMAL(18,6)", nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    Actual = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalArea",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalArea", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    AreaId = table.Column<long>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Logo_SmallSize = table.Column<string>(nullable: true),
                    Logo_MediumSize = table.Column<string>(nullable: true),
                    Logo_OriginalSize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Employer_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Laboring = table.Column<bool>(nullable: true),
                    ProfessionalAreaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Specialization_ProfessionalArea_ProfessionalAreaId",
                        column: x => x.ProfessionalAreaId,
                        principalTable: "ProfessionalArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    KeySkills = table.Column<string>(nullable: true),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_Building = table.Column<string>(nullable: true),
                    Address_Description = table.Column<string>(nullable: true),
                    Address_Latitude = table.Column<decimal>(type: "DECIMAL(18,6)", nullable: true),
                    Address_Longitude = table.Column<decimal>(type: "DECIMAL(18,6)", nullable: true),
                    Schedule = table.Column<int>(nullable: true),
                    AcceptHandicapped = table.Column<bool>(nullable: false),
                    AcceptKids = table.Column<bool>(nullable: false),
                    Experience = table.Column<int>(nullable: true),
                    Employment = table.Column<int>(nullable: true),
                    Salary_From = table.Column<decimal>(type: "DECIMAL(18,6)", nullable: true),
                    Salary_To = table.Column<decimal>(type: "DECIMAL(18,6)", nullable: true),
                    Salary_Currency = table.Column<string>(nullable: true),
                    Salary_Gross = table.Column<bool>(nullable: true),
                    Archived = table.Column<bool>(nullable: false),
                    PublishedAt = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<long>(nullable: true),
                    Contact_Name = table.Column<string>(nullable: true),
                    Contact_Email = table.Column<string>(nullable: true),
                    Contact_PhoneNumbers = table.Column<string>(nullable: true),
                    HasTest = table.Column<bool>(nullable: false),
                    TestRequired = table.Column<bool>(nullable: true),
                    EmployerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancy_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacancy_Employer_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacancySpecializationLink",
                columns: table => new
                {
                    VacancyId = table.Column<long>(nullable: false),
                    SpecializationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancySpecializationLink", x => new { x.SpecializationId, x.VacancyId });
                    table.ForeignKey(
                        name: "FK_VacancySpecializationLink_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancySpecializationLink_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_ExternalId",
                table: "Area",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code",
                table: "Currency",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ExternalId",
                table: "Department",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_AreaId",
                table: "Employer",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employer_ExternalId",
                table: "Employer",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalArea_ExternalId",
                table: "ProfessionalArea",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Specialization_ExternalId",
                table: "Specialization",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Specialization_ProfessionalAreaId",
                table: "Specialization",
                column: "ProfessionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_DepartmentId",
                table: "Vacancy",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_EmployerId",
                table: "Vacancy",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_ExternalId",
                table: "Vacancy",
                column: "ExternalId",
                unique: true,
                filter: "[ExternalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySpecializationLink_SpecializationId",
                table: "VacancySpecializationLink",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancySpecializationLink_VacancyId",
                table: "VacancySpecializationLink",
                column: "VacancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "VacancySpecializationLink");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "Vacancy");

            migrationBuilder.DropTable(
                name: "ProfessionalArea");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Employer");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
