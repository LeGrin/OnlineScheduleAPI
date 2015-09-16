namespace DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subjects", "Classroom");
            DropColumn("dbo.Subjects", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subjects", "Classroom", c => c.String());
        }
    }
}
