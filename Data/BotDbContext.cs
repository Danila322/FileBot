using FileBot.Models;
using Microsoft.EntityFrameworkCore;

namespace FileBot.Data
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options) : base(options)
        {
        }

        public DbSet<Directory> Directories { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<UserInfo> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInfo>().HasKey(u => u.UserId);
            builder.Entity<UserInfo>().Property(u => u.UserId).ValueGeneratedNever();
            builder.Entity<UserInfo>().HasOne(u => u.CurrentDirectory).WithOne().HasForeignKey<UserInfo>(u => u.CurrentDirectoryId);

            builder.Entity<File>().HasKey(f => f.Id);
            builder.Entity<File>().HasOne(f => f.Directory).WithMany(d => d.Files).HasForeignKey(f => f.DirectoryId);

            builder.Entity<Directory>().HasKey(d => d.Id);
            builder.Entity<Directory>().HasOne(d => d.Parent).WithMany(d => d.Directories).HasForeignKey(d => d.ParentId);
        }
    }
}
