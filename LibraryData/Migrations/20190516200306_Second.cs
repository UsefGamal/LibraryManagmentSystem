using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryCheckoutAssetId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryCards_LibraryCheckoutCardId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryHoldAssetId",
                table: "Holds");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryCards_LibraryHoldCardId",
                table: "Holds");

            migrationBuilder.RenameColumn(
                name: "LibraryHoldCardId",
                table: "Holds",
                newName: "LibraryCardId");

            migrationBuilder.RenameColumn(
                name: "LibraryHoldAssetId",
                table: "Holds",
                newName: "LibraryAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Holds_LibraryHoldCardId",
                table: "Holds",
                newName: "IX_Holds_LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Holds_LibraryHoldAssetId",
                table: "Holds",
                newName: "IX_Holds_LibraryAssetId");

            migrationBuilder.RenameColumn(
                name: "LibraryCheckoutCardId",
                table: "Checkouts",
                newName: "LibraryCardId");

            migrationBuilder.RenameColumn(
                name: "LibraryCheckoutAssetId",
                table: "Checkouts",
                newName: "LibraryAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_LibraryCheckoutCardId",
                table: "Checkouts",
                newName: "IX_Checkouts_LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_LibraryCheckoutAssetId",
                table: "Checkouts",
                newName: "IX_Checkouts_LibraryAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryAssetId",
                table: "Checkouts",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryCards_LibraryCardId",
                table: "Checkouts",
                column: "LibraryCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryAssetId",
                table: "Holds",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryCards_LibraryCardId",
                table: "Holds",
                column: "LibraryCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryAssetId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryCards_LibraryCardId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryAssetId",
                table: "Holds");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryCards_LibraryCardId",
                table: "Holds");

            migrationBuilder.RenameColumn(
                name: "LibraryCardId",
                table: "Holds",
                newName: "LibraryHoldCardId");

            migrationBuilder.RenameColumn(
                name: "LibraryAssetId",
                table: "Holds",
                newName: "LibraryHoldAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Holds_LibraryCardId",
                table: "Holds",
                newName: "IX_Holds_LibraryHoldCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Holds_LibraryAssetId",
                table: "Holds",
                newName: "IX_Holds_LibraryHoldAssetId");

            migrationBuilder.RenameColumn(
                name: "LibraryCardId",
                table: "Checkouts",
                newName: "LibraryCheckoutCardId");

            migrationBuilder.RenameColumn(
                name: "LibraryAssetId",
                table: "Checkouts",
                newName: "LibraryCheckoutAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_LibraryCardId",
                table: "Checkouts",
                newName: "IX_Checkouts_LibraryCheckoutCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Checkouts_LibraryAssetId",
                table: "Checkouts",
                newName: "IX_Checkouts_LibraryCheckoutAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryCheckoutAssetId",
                table: "Checkouts",
                column: "LibraryCheckoutAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryCards_LibraryCheckoutCardId",
                table: "Checkouts",
                column: "LibraryCheckoutCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryHoldAssetId",
                table: "Holds",
                column: "LibraryHoldAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryCards_LibraryHoldCardId",
                table: "Holds",
                column: "LibraryHoldCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
