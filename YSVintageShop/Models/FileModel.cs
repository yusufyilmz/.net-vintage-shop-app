using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YSVintageShop.Utils;

namespace YSVintageShop.Models
{
    [Table("Files")]

    public class FileModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        public int? BrandId { get; set; }
        public virtual BrandModel Brand { get; set; }

        public int? CollectionId { get; set; }
        public virtual CollectionModel Collection { get; set; }

        public int? ProductId { get; set; }
        public virtual ProductModel Product { get; set; }

        public int? UserId { get; set; }
        public virtual UserModel User { get; set; }

    }
}