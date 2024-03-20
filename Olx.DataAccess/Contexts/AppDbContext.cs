using Microsoft.EntityFrameworkCore;
using Olx.Domain.Commons;
using Olx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olx.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    DbSet<User> Users;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User model
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .Property(u => u.Balance)
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0);

        modelBuilder.Entity<User>()
            .Property(u => u.IsVip)
            .HasDefaultValue(false);

        modelBuilder.Entity<User>()
            .Property(u => u.ProfilePicture)
            .IsRequired()
            .HasColumnType("bytea");

        modelBuilder.Entity<User>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<User>()
            .Property(a => a.UpdatedAt);

        modelBuilder.Entity<User>()
            .Property(a => a.DeletedAt);

        modelBuilder.Entity<User>()
            .Property(a => a.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////

        //----------------------------------------------------------------

        // Category model

        modelBuilder.Entity<Category>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Category>()
            .Property(c => c.ParentId);

        modelBuilder.Entity<Category>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Category>()
            .Property(a => a.UpdatedAt);

        modelBuilder.Entity<Category>()
            .Property(a => a.DeletedAt);

        modelBuilder.Entity<Category>()
            .Property(a => a.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////

        //----------------------------------------------------------------

        // Property model

        modelBuilder.Entity<Property>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Property>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Property>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Property>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Property>()
            .Property(a => a.UpdatedAt);

        modelBuilder.Entity<Property>()
            .Property(a => a.DeletedAt);

        modelBuilder.Entity<Property>()
            .Property(a => a.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////

        //----------------------------------------------------------------

        // PropertyValue model

        modelBuilder.Entity<PropertyValue>()
        .HasKey(pv => pv.Id);

        modelBuilder.Entity<PropertyValue>()
            .Property(pv => pv.Value)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<PropertyValue>()
            .Property(pv => pv.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<PropertyValue>()
            .Property(pv => pv.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<PropertyValue>()
            .Property(pv => pv.UpdatedAt);

        modelBuilder.Entity<PropertyValue>()
            .Property(pv => pv.DeletedAt);

        modelBuilder.Entity<PropertyValue>()
            .Property(pv => pv.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////

        //----------------------------------------------------------------

        // PostProperty model

        modelBuilder.Entity<PostProperty>()
        .HasKey(pp => pp.Id);

        modelBuilder.Entity<PostProperty>()
            .HasOne(pp => pp.Post)
            .WithMany()
            .HasForeignKey(pp => pp.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PostProperty>()
            .HasOne(pp => pp.Property)
            .WithMany()
            .HasForeignKey(pp => pp.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PostProperty>()
            .HasOne(pp => pp.Value)
            .WithMany()
            .HasForeignKey(pp => pp.ValueId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PostProperty>()
            .Property(pp => pp.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<PostProperty>()
            .Property(pp => pp.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<PostProperty>()
            .Property(pp => pp.UpdatedAt);

        modelBuilder.Entity<PostProperty>()
            .Property(pp => pp.DeletedAt);

        modelBuilder.Entity<PostProperty>()
            .Property(pp => pp.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////

        //----------------------------------------------------------------

        // Post model

        modelBuilder.Entity<Post>()
        .HasKey(p => p.Id);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Post>()
            .Property(p => p.Description)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

        modelBuilder.Entity<Post>()
            .Property(p => p.CityOrRegion)
            .HasMaxLength(100);

        modelBuilder.Entity<Post>()
            .Property(p => p.District)
            .HasMaxLength(100);

        modelBuilder.Entity<Post>()
            .Property(p => p.IsLeft)
            .HasDefaultValue(true);

        modelBuilder.Entity<Post>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Post>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Post>()
            .Property(p => p.UpdatedAt);

        modelBuilder.Entity<Post>()
            .Property(p => p.DeletedAt);

        modelBuilder.Entity<Post>()
            .Property(p => p.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////
        //----------------------------------------------------------------

        // FavoritePost model

        modelBuilder.Entity<FavouritePost>()
            .HasKey(fp => new { fp.UserId, fp.PostId });

        modelBuilder.Entity<FavouritePost>()
            .HasOne(fp => fp.User)
            .WithMany()
            .HasForeignKey(fp => fp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FavouritePost>()
            .HasOne(fp => fp.Post)
            .WithMany()
            .HasForeignKey(fp => fp.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FavouritePost>()
            .Property(fp => fp.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<FavouritePost>()
            .Property(fp => fp.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<FavouritePost>()
            .Property(fp => fp.UpdatedAt);

        modelBuilder.Entity<FavouritePost>()
            .Property(fp => fp.DeletedAt);

        modelBuilder.Entity<FavouritePost>()
            .Property(fp => fp.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////
        //----------------------------------------------------------------

        // Message model 

        modelBuilder.Entity<Message>()
        .HasKey(m => m.Id);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Post)
            .WithMany()
            .HasForeignKey(m => m.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .Property(m => m.Content)
            .IsRequired();

        modelBuilder.Entity<Message>()
            .Property(m => m.SendDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Message>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Message>()
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Message>()
            .Property(m => m.UpdatedAt);

        modelBuilder.Entity<Message>()
            .Property(m => m.DeletedAt);

        modelBuilder.Entity<Message>()
            .Property(m => m.IsDeleted)
            .HasDefaultValue(false);

        // Ended //////////////////////////////////
        //----------------------------------------------------------------
    }
}
