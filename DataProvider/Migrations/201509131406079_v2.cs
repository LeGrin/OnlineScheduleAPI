namespace DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Groups", new[] { "Name" });
            AlterColumn("dbo.Groups", "Name", c => c.String());
            AlterColumn("dbo.Groups", "Key", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.Groups", "Key", unique: true, name: "IX_GroupKey");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Groups", "IX_GroupKey");
            AlterColumn("dbo.Groups", "Key", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.Groups", "Name", unique: true);
        }
    }
}
