using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace plazzo_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Goods",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Deed",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Surface",
                table: "Goods");

            migrationBuilder.RenameTable(
                name: "Goods",
                newName: "Property");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Property",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Property",
                newName: "ConstructionYear");

            migrationBuilder.RenameColumn(
                name: "Rooms",
                table: "Property",
                newName: "RoomCount");

            migrationBuilder.RenameColumn(
                name: "DPE",
                table: "Property",
                newName: "EnergyRating");

            migrationBuilder.RenameColumn(
                name: "Construction_Date",
                table: "Property",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Bedrooms",
                table: "Property",
                newName: "BedroomCount");

            migrationBuilder.RenameColumn(
                name: "Bathrooms",
                table: "Property",
                newName: "BathroomCount");

            migrationBuilder.RenameColumn(
                name: "Agency_Id",
                table: "Property",
                newName: "CommercialId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Property",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "Property",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SurfaceArea",
                table: "Property",
                type: "numeric(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PropertyAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAddresses_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<byte>(type: "smallint", nullable: false),
                    Caption = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyPhotos_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_AgencyId",
                table: "Property",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_CommercialId",
                table: "Property",
                column: "CommercialId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAddresses_PropertyId",
                table: "PropertyAddresses",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_PropertyId",
                table: "PropertyFeatures",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPhotos_PropertyId",
                table: "PropertyPhotos",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Agencies_AgencyId",
                table: "Property",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Users_CommercialId",
                table: "Property",
                column: "CommercialId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Agencies_AgencyId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Users_CommercialId",
                table: "Property");

            migrationBuilder.DropTable(
                name: "PropertyAddresses");

            migrationBuilder.DropTable(
                name: "PropertyFeatures");

            migrationBuilder.DropTable(
                name: "PropertyPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_AgencyId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_CommercialId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "SurfaceArea",
                table: "Property");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Goods");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Goods",
                newName: "Construction_Date");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Goods",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RoomCount",
                table: "Goods",
                newName: "Rooms");

            migrationBuilder.RenameColumn(
                name: "EnergyRating",
                table: "Goods",
                newName: "DPE");

            migrationBuilder.RenameColumn(
                name: "ConstructionYear",
                table: "Goods",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "CommercialId",
                table: "Goods",
                newName: "Agency_Id");

            migrationBuilder.RenameColumn(
                name: "BedroomCount",
                table: "Goods",
                newName: "Bedrooms");

            migrationBuilder.RenameColumn(
                name: "BathroomCount",
                table: "Goods",
                newName: "Bathrooms");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Goods",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "Deed",
                table: "Goods",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Surface",
                table: "Goods",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goods",
                table: "Goods",
                column: "Id");
        }
    }
}
