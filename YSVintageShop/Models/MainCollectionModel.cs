using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSVintageShop.Models
{
    [Table("MainCollections")]

    public class MainCollectionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MainCollectionId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        public int Status { get; set; }

        public virtual ICollection<FileModel> Files { get; set; }

        public virtual ICollection<CollectionModel> Collections { get; set; }

    }
}
