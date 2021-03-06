namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PushMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PushMessages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Mensagem = c.String(),
                        Agent = c.String(),
                        Microsoft = c.Boolean(nullable: false),
                        Android = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PushMessages");
        }
    }
}
