namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IotActivities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "iot_tdc_2014.AgendaEventoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Palestrante = c.String(),
                        MiniBioPalestrante = c.String(),
                        UrlSitePalestrante = c.String(),
                        TwitterPalestrante = c.String(),
                        UrlFotoPalestrante = c.String(),
                        Trilha = c.String(),
                        Titulo = c.String(),
                        Descricao = c.String(),
                        Data = c.DateTime(nullable: false),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraTermino = c.DateTime(nullable: false),
                        CreatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("iot_tdc_2014.AgendaEventoes");
        }
    }
}
