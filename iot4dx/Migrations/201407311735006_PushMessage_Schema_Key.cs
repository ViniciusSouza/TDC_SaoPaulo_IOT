namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PushMessage_Schema_Key : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PushMessages", newName: "PushMessage");
            MoveTable(name: "dbo.PushMessage", newSchema: "iot_tdc_2014");
        }
        
        public override void Down()
        {
            MoveTable(name: "iot_tdc_2014.PushMessage", newSchema: "dbo");
            RenameTable(name: "dbo.PushMessage", newName: "PushMessages");
        }
    }
}
