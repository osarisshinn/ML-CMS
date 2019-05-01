using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaerskLine.CMS.Models.Entities
{
    public class Notification
    {
        [Key]
        public virtual long Id { get; set; }
        [Required]
        public virtual string Message { get; set; }
        [Required]
        public virtual DateTime DateTime { get; set; }

        public virtual long? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}