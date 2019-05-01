using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskLine.CMS.Models
{
    public class ShippingInfoViewModel
    {
        public long OrderId { get; set; }

        [Required(ErrorMessage ="Please fill in address of where to pickup the goods from."), Display(Name = "From Address")]
        public string FromAddress { get; set; }

        [Required(ErrorMessage = "Please fill in address of where to deliver the goods to."), Display(Name = "To Address")]
        public string ToAddress { get; set; }

        [Required(ErrorMessage ="Please select item's category"), Display(Name = "Item Category")]
        public long ItemCategoryId { get; set; }

        [Required(ErrorMessage = "Please fill in item's name."), Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please fill in item's weight."), Display(Name = "Item Weight")]
        public int ItemWeight { get; set; }
        [Required(ErrorMessage = "Please fill in when to pickup the items from you."), Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }
        public string Status { get; set; }
        public string OrderTrackingId { get; set; }

        public IList<Entities.ItemCategory> ItemCategories { get; set; }
    }
}