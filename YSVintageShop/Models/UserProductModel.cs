using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YSVintageShop.Models;

namespace YSVintageShop.Models
{
    [Table("UserProductRelation")]
    public class UserProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        //public string RelatedProductId { get; set; }

        public int UserProductRelationType { get; set; }

        public virtual ICollection<ProductModel> RelatedProducts { get; set; }

    }

}