namespace DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Groups", name: "Creator_Id", newName: "CreatorId");
            RenameIndex(table: "dbo.Groups", name: "IX_Creator_Id", newName: "IX_CreatorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Groups", name: "IX_CreatorId", newName: "IX_Creator_Id");
            RenameColumn(table: "dbo.Groups", name: "CreatorId", newName: "Creator_Id");
        }
    }
}
