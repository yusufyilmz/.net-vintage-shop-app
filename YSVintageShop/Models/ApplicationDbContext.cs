using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace YSVintageShop.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("YSContext", throwIfV1Schema: false)
        {
        }
        
        public DbSet<CollectionModel> Collections { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<FileModel> Files { get; set; }

        //public DbSet<UserProductModel2> UserProducts { get; set; }

        public DbSet<MainCollectionModel> MainCollections { get; set; }

        public DbSet<MainHeadModel> MainHeads { get; set; }

        public DbSet<UserUserModel> UserUsers { get; set; }

        public DbSet<UserProductModel> UserProduct { get; set; }

        public DbSet<UserModel> UserDetail { get; set; }




        //UserProductModel

        //public DbSet<UserModel> Users { get; set; }

        //public DbSet<ApplicationUser> ApplicationUser { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //    {
        //        //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //        modelBuilder.Entity<BrandModel>()
        //.HasRequired(c => c.Products)
        //.WithMany()
        //.WillCascadeOnDelete(false);

        //        modelBuilder.Entity<CollectionModel>()
        //            .HasRequired(s => s.Products)
        //            .WithMany()
        //            .WillCascadeOnDelete(false);


        //        //modelBuilder.Entity<Course>()
        //        //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
        //        //    .Map(t => t.MapLeftKey("CourseID")
        //        //        .MapRightKey("InstructorID")
        //        //        .ToTable("CourseInstructor"));

        //        //modelBuilder.Entity<Department>().MapToStoredProcedures();
        //    }



    }
}