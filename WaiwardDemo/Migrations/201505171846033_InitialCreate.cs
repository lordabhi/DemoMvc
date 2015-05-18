namespace WaiwardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiscrepancyTypes",
                c => new
                    {
                        DiscrepancyTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DiscrepancyTypeID);
            
            CreateTable(
                "dbo.HseqCaseFiles",
                c => new
                    {
                        HseqCaseFileID = c.Int(nullable: false, identity: true),
                        CaseNo = c.Int(nullable: false),
                        NcrID = c.Int(),
                    })
                .PrimaryKey(t => t.HseqCaseFileID);
            
            CreateTable(
                "dbo.Ncrs",
                c => new
                    {
                        NcrID = c.Int(nullable: false, identity: true),
                        NcrSource = c.Int(nullable: false),
                        NcrState = c.Int(nullable: false),
                        DiscrepancyTypeID = c.Int(nullable: false),
                        HseqCaseFileID = c.Int(),
                        Title = c.String(),
                        Description = c.String(),
                        RecordType = c.String(),
                        EnteredBy = c.String(),
                        ReportedBy = c.String(),
                        QualityCoordinator = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.NcrID)
                .ForeignKey("dbo.DiscrepancyTypes", t => t.DiscrepancyTypeID, cascadeDelete: true)
                .ForeignKey("dbo.HseqCaseFiles", t => t.HseqCaseFileID)
                .Index(t => t.DiscrepancyTypeID)
                .Index(t => t.HseqCaseFileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ncrs", "HseqCaseFileID", "dbo.HseqCaseFiles");
            DropForeignKey("dbo.Ncrs", "DiscrepancyTypeID", "dbo.DiscrepancyTypes");
            DropIndex("dbo.Ncrs", new[] { "HseqCaseFileID" });
            DropIndex("dbo.Ncrs", new[] { "DiscrepancyTypeID" });
            DropTable("dbo.Ncrs");
            DropTable("dbo.HseqCaseFiles");
            DropTable("dbo.DiscrepancyTypes");
        }
    }
}
