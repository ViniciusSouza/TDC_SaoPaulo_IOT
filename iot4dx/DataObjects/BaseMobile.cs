using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iot4dx.DataObjects
{
    public abstract class BaseMobile
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Index(IsClustered = true)]
        public DateTimeOffset? CreatedAt { get; set; }
        public bool Deleted { get; set; }
        //[Key]
        //public string Id { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset? UpdatedAt { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
    }
}