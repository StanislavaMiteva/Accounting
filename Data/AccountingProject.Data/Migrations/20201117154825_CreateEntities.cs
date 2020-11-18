namespace AccountingProject.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GLAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    DebitBalance = table.Column<decimal>(nullable: false),
                    CreditBalance = table.Column<decimal>(nullable: false),
                    IsInventory = table.Column<bool>(nullable: false),
                    IsFixedAsset = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    VAT = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 30, nullable: true),
                    CityId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counterparties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counterparties_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticalAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    GLAccountId = table.Column<int>(nullable: false),
                    DebitBalance = table.Column<decimal>(nullable: false),
                    CreditBalance = table.Column<decimal>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticalAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyticalAccounts_GLAccounts_GLAccountId",
                        column: x => x.GLAccountId,
                        principalTable: "GLAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FixedAssets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    InventoryNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    AcquisitionPrice = table.Column<decimal>(nullable: false),
                    AcquisitionDate = table.Column<DateTime>(nullable: false),
                    DerecognitionDate = table.Column<DateTime>(nullable: false),
                    UsefulLife = table.Column<int>(nullable: false),
                    SalvageValue = table.Column<decimal>(nullable: false),
                    DepreciationMethod = table.Column<int>(nullable: false),
                    AccountablePerson = table.Column<string>(maxLength: 50, nullable: true),
                    GLAccountId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixedAssets_GLAccounts_GLAccountId",
                        column: x => x.GLAccountId,
                        principalTable: "GLAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Measure = table.Column<string>(maxLength: 10, nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    GLAccountId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_GLAccounts_GLAccountId",
                        column: x => x.GLAccountId,
                        principalTable: "GLAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 30, nullable: false),
                    DebitGLAccountId = table.Column<int>(nullable: false),
                    DebitAnalyticalAccountId = table.Column<int>(nullable: true),
                    CreditGLAccountId = table.Column<int>(nullable: false),
                    CreditAnalyticalAccountId = table.Column<int>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CounterpartyId = table.Column<int>(nullable: true),
                    CreatorId = table.Column<string>(nullable: false),
                    IsPurchase = table.Column<bool>(nullable: false),
                    IsSale = table.Column<bool>(nullable: false),
                    Folder = table.Column<string>(maxLength: 10, nullable: true),
                    ConsecutiveNumber = table.Column<string>(maxLength: 10, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Counterparties_CounterpartyId",
                        column: x => x.CounterpartyId,
                        principalTable: "Counterparties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_AnalyticalAccounts_CreditAnalyticalAccountId",
                        column: x => x.CreditAnalyticalAccountId,
                        principalTable: "AnalyticalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_GLAccounts_CreditGLAccountId",
                        column: x => x.CreditGLAccountId,
                        principalTable: "GLAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_AnalyticalAccounts_DebitAnalyticalAccountId",
                        column: x => x.DebitAnalyticalAccountId,
                        principalTable: "AnalyticalAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_GLAccounts_DebitGLAccountId",
                        column: x => x.DebitGLAccountId,
                        principalTable: "GLAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalAccounts_GLAccountId",
                table: "AnalyticalAccounts",
                column: "GLAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalAccounts_IsDeleted",
                table: "AnalyticalAccounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IsDeleted",
                table: "Cities",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Counterparties_CityId",
                table: "Counterparties",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Counterparties_IsDeleted",
                table: "Counterparties",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_IsDeleted",
                table: "DocumentTypes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssets_GLAccountId",
                table: "FixedAssets",
                column: "GLAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssets_IsDeleted",
                table: "FixedAssets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccounts_IsDeleted",
                table: "GLAccounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_GLAccountId",
                table: "Inventories",
                column: "GLAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_IsDeleted",
                table: "Inventories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CounterpartyId",
                table: "Transactions",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatorId",
                table: "Transactions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditAnalyticalAccountId",
                table: "Transactions",
                column: "CreditAnalyticalAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditGLAccountId",
                table: "Transactions",
                column: "CreditGLAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebitAnalyticalAccountId",
                table: "Transactions",
                column: "DebitAnalyticalAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebitGLAccountId",
                table: "Transactions",
                column: "DebitGLAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DocumentTypeId",
                table: "Transactions",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IsDeleted",
                table: "Transactions",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixedAssets");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Counterparties");

            migrationBuilder.DropTable(
                name: "AnalyticalAccounts");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "GLAccounts");
        }
    }
}
