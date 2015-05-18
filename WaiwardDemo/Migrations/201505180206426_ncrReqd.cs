namespace WaiwardDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ncrReqd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ncrs", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Ncrs", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ncrs", "Description", c => c.String());
            AlterColumn("dbo.Ncrs", "Title", c => c.String());
        }
    }
}
