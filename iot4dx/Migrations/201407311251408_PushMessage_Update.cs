namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PushMessage_Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PushMessages", "Mensagem", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PushMessages", "Mensagem", c => c.String());
        }
    }
}
