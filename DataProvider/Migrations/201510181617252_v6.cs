namespace DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "ParentGroupId", c => c.Int());
            CreateIndex("dbo.Groups", "ParentGroupId");
            AddForeignKey("dbo.Groups", "ParentGroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "ParentGroupId", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "ParentGroupId" });
            DropColumn("dbo.Groups", "ParentGroupId");
        }
    }
}
