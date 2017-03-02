using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSVintageShop.Models
{
    [Table("Users")]
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<FileModel> Files { get; set; }

        public int? UserUserId { get; set; }
        public virtual UserProductModel UserUser { get; set; }

    }
}
