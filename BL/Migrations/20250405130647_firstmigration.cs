using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />

        //after scaffolding i want to link database with migration without creating database again
        //so we delete migration up & down methods content then update-database
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
