using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test2.Models
{
    [System.Data.Entity.DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class WebsiteDbContext : DbContext
    {
        static WebsiteDbContext() { Database.SetInitializer(new MyInt()); }
        public WebsiteDbContext()
            : base("name=DefaultConnection") { }
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<UserClaim> Claims { get; set; }
        //public DbSet<Content> Contents { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<IdentityGroup> Groups { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>()
                .HasMany(n => n.Roles)
                .WithMany(n => n.Users)
                .Map(n => n.ToTable("users_roles"));
            modelBuilder.Entity<Content>()
                .HasMany(n => n.ContentTypes)
                .WithMany(n => n.Contents)
                .Map(n => n.ToTable("contents_types_ref"));
            modelBuilder.Entity<IdentityGroup>()
                .HasMany(n => n.Roles)
                .WithMany(n => n.Groups)
                .Map(n => n.ToTable("group_roles"));
            base.OnModelCreating(modelBuilder);
        }
    }
    public class MyInt : DropCreateDatabaseAlways<WebsiteDbContext>
    {
        protected override void Seed(WebsiteDbContext context)
        {
            context.SaveChanges();
            base.Seed(context);
        }
    }
}