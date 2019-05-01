using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaerskLine.CMS.Models.Entities
{
    public class ItemCategory
    {
        [Key]
        public virtual long Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
    }
}