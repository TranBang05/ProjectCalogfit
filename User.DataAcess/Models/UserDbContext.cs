using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace User.DataAcess.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permision> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "server=LAPTOP-0LAB0G9K\\TXB_SQLLAP;database=UsersDb;user=sa;password=123;TrustServerCertificate=true";
                optionsBuilder.UseSqlServer(conStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                

                entity.HasMany(e => e.UserRoles)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);

                
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                

                entity.HasMany(e => e.UserRoles)
                      .WithOne(e => e.Role)
                      .HasForeignKey(e => e.RoleId);

                entity.HasMany(e => e.RolePermissions)
                      .WithOne(e => e.Role)
                      .HasForeignKey(e => e.RoleId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);
            });

            modelBuilder.Entity<Permision>(entity =>
            {
                entity.HasKey(e => e.PermissionId);
               

                entity.HasMany(e => e.RolePermissions)
                      .WithOne(e => e.Permissions)
                      .HasForeignKey(e => e.PermissionId);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => e.RolePermissionId);
            });

           
        }

    }
}
