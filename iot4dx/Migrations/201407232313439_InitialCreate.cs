namespace iot4dx.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "iot_tdc_2014.IoTActivities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Color = c.Int(nullable: false),
                        User = c.String(),
                        UriImage = c.String(),
                        Complete = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(precision: 7),
                        Deleted = c.Boolean(nullable: false),
                        UpdatedAt = c.DateTimeOffset(precision: 7),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("iot_tdc_2014.IoTActivities");
        }
    }
}
