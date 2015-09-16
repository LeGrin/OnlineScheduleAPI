namespace DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleRows", "EventId", "dbo.Events");
            DropForeignKey("dbo.ScheduleRows", "ScheduleRuleId", "dbo.ScheduleRules");
            DropIndex("dbo.ScheduleRows", new[] { "ScheduleRuleId" });
            DropIndex("dbo.ScheduleRows", new[] { "EventId" });
            DropColumn("dbo.ScheduleRules", "Lecturer");
            DropTable("dbo.ScheduleRows");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScheduleRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ScheduleRuleId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        DurationInMinutes = c.Int(nullable: false),
                        Room = c.String(),
                        Lecturer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ScheduleRules", "Lecturer", c => c.String());
            CreateIndex("dbo.ScheduleRows", "EventId");
            CreateIndex("dbo.ScheduleRows", "ScheduleRuleId");
            AddForeignKey("dbo.ScheduleRows", "ScheduleRuleId", "dbo.ScheduleRules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScheduleRows", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
