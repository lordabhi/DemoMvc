namespace WaiwardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ncrReqd1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.HseqCaseFiles", "CaseNo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.HseqCaseFiles", new[] { "CaseNo" });
        }
    }
}
