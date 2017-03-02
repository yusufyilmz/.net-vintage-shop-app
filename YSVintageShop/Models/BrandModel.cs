using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSVintageShop.Models
{
    [Table("Brands")]

    public class BrandModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BrandId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        public int Status { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
        public virtual ICollection<FileModel> Files { get; set; }


    }
}
