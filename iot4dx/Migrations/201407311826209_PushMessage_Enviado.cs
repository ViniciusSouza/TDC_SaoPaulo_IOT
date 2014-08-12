namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PushMessage_Enviado : DbMigration
    {
        public override void Up()
        {
            AddColumn("iot_tdc_2014.PushMessage", "Enviado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("iot_tdc_2014.PushMessage", "Enviado");
        }
    }
}
