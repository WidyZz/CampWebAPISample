using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampWebAPISample.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    SpeakerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.SpeakerId);
                });

            migrationBuilder.CreateTable(
                name: "Camps",
                columns: table => new
                {
                    CampId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moniker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camps", x => x.CampId);
                    table.ForeignKey(
                        name: "FK_Camps_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talks",
                columns: table => new
                {
                    TalkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampId = table.Column<int>(type: "int", nullable: false),
                    SpeakerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talks", x => x.TalkId);
                    table.ForeignKey(
                        name: "FK_Talks_Camps_CampId",
                        column: x => x.CampId,
                        principalTable: "Camps",
                        principalColumn: "CampId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Talks_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "LocationId", "Address", "City", "Country", "PostalCode" },
                values: new object[] { 1, "Adresa 123", "Praha", "Czech Republic", "111 50" });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "SpeakerId", "Company", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "Unicorn", "radek.garzina@gmail.com", "Radek", "Garzina" });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "SpeakerId", "Company", "Email", "FirstName", "LastName" },
                values: new object[] { 2, "Microsoft", "bill.gates@microsoft.com", "Bill", "Gates" });

            migrationBuilder.InsertData(
                table: "Camps",
                columns: new[] { "CampId", "EventDate", "Length", "LocationId", "Moniker", "Name" },
                values: new object[] { 1, new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Hatchery2022-2", ".NET Hatchery  2022" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "TalkId", "Abstract", "CampId", "SpeakerId", "Title" },
                values: new object[] { 1, "bla bla bla", 1, 1, "ASP.NET Core WebAPI" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "TalkId", "Abstract", "CampId", "SpeakerId", "Title" },
                values: new object[] { 2, "bla bla bla", 1, 2, "C# Fundamentals" });

            migrationBuilder.CreateIndex(
                name: "IX_Camps_LocationId",
                table: "Camps",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_CampId",
                table: "Talks",
                column: "CampId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_SpeakerId",
                table: "Talks",
                column: "SpeakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talks");

            migrationBuilder.DropTable(
                name: "Camps");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
