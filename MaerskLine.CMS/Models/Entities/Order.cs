using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaerskLine.CMS.Models.Entities
{
    public class Order
    {
        [Key]
        public virtual long Id { get; set; }
        [Required]
        public virtual string ItemName { get; set; }
        public virtual int ItemWeight { get; set; }

        public virtual long ItemCategoryId { get; set; }
        [ForeignKey("ItemCategoryId")]
        public virtual ItemCategory ItemCategory { get; set; }

        [Required]
        public virtual string FromAddress { get; set; }
        [Required]
        public virtual string ToAddress { get; set; }
        [Required]
        public virtual DateTime PickupDate { get; set; }
        public virtual DateTime? ArrivedDate { get; set; }
        public virtual DateTime? DepartureDate { get; set; }
        [Required]
        public virtual string Status { get; set; }

        public virtual string OrderTrackingId { get; set; }

        [Required]
        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}