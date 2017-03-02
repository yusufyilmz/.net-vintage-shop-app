using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSVintageShop.Models
{
    [Table("Products")]

    public class ProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Details { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        //public int CollectionId { get; set; }

        //public int BrandId { get; set; }

        public int Status { get; set; }

        public double Price { get; set; }

        public double OriginalPrice { get; set; }

        public int UsageStatus { get; set; }

        public int Stock { get; set; }

        public int BrandId { get; set; }

        public virtual BrandModel Brand { get; set; }

        public int CollectionId { get; set; }

        public virtual CollectionModel Collection { get; set; }

        public virtual ICollection<FileModel> Files { get; set; }

        public int? UserProductId { get; set; }
        public virtual UserProductModel UserProduct { get; set; }

    }
}
