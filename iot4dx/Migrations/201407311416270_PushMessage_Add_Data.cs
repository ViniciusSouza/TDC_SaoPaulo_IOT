namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PushMessage_Add_Data : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PushMessages", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PushMessages", "Data");
        }
    }
}
