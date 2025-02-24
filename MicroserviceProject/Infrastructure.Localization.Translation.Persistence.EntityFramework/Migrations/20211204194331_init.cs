﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Localization.Translation.Persistence.EntityFramework.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TRANSLATIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KEY = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: true),
                    TEXT = table.Column<string>(type: "NVARCHAR(4000)", maxLength: 4000, nullable: true),
                    LANGUAGECODE = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    CREATEDATE = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UPDATEDATE = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DELETEDATE = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSLATIONS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRANSLATIONS_KEY_LANGUAGECODE",
                table: "TRANSLATIONS",
                columns: new[] { "KEY", "LANGUAGECODE" },
                unique: true,
                filter: "[KEY] IS NOT NULL AND [LANGUAGECODE] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRANSLATIONS");
        }
    }
}
