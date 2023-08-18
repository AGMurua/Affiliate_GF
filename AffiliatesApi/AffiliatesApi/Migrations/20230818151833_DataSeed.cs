using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffiliatesApi.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(table: "Affiliate",
                                        columns: new[] { "Id", "Name" },
                                        values: new object[] { 1, "Francis Ford Coppola" });
            migrationBuilder.InsertData(table: "Affiliate",
                                        columns: new[] { "Id", "Name" },
                                        values: new object[] { 2, "Quentin Tarantino" });


            migrationBuilder.InsertData(table: "Customer",
                                        columns: new[] { "Id", "AffiliateId", "Name" },
                                        values: new object[] { 1,1, "Robert De Niro" });
            migrationBuilder.InsertData(table: "Customer",
                                        columns: new[] { "Id", "AffiliateId", "Name" },
                                        values: new object[] { 2, 1, "Mario Puzo" });
            migrationBuilder.InsertData(table: "Customer",
                                        columns: new[] { "Id", "AffiliateId", "Name" },
                                        values: new object[] { 3, 1, "Al Pacino" });

            migrationBuilder.InsertData(table: "Customer",
                                        columns: new[] { "Id", "AffiliateId", "Name" },
                                        values: new object[] { 4, 2, "Samuel L. Jackson" });
            migrationBuilder.InsertData(table: "Customer",
                                        columns: new[] { "Id", "AffiliateId", "Name" },
                                        values: new object[] { 5, 2, "Uma Thurman" });
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Customer where Id <= 5", true);
            migrationBuilder.Sql("DELETE FROM Affiliate where Id <= 2", true);
        }
    }



}
