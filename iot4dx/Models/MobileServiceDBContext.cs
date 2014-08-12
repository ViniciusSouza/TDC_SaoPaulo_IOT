using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iot4dx.Models
{
    public class MobileServiceDBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MobileServiceDBContext() : base("name=MobileServiceDBContext")
        {
        }

        public System.Data.Entity.DbSet<iot4dx.DataObjects.IoTActivity> IoTActivities { get; set; }

        public System.Data.Entity.DbSet<iot4dx.DataObjects.AgendaEvento> AgendaEventoes { get; set; }

        public System.Data.Entity.DbSet<iot4dx.DataObjects.PushMessage> PushMessages { get; set; }
    
    }
}
