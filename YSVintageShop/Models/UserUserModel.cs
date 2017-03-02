using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YSVintageShop.Models;

namespace YSVintageShop.Models
{
    [Table("UserUserRelation")]
    public class UserUserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        //public string RelatedUserId { get; set; }

        public int UserUserRelationType { get; set; }

        public virtual ICollection<UserModel> RelatedUsers { get; set; }

    }
}