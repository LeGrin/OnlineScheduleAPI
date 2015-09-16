namespace DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Classroom = c.String(),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Events", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "SubjectId");
            AddForeignKey("dbo.Events", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            DropColumn("dbo.Events", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Name", c => c.String());
            DropForeignKey("dbo.Events", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.Subjects", new[] { "TeacherId" });
            DropIndex("dbo.Events", new[] { "SubjectId" });
            DropColumn("dbo.Events", "SubjectId");
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
        }
    }
}
