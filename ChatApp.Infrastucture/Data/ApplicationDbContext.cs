using System.Text.RegularExpressions;
using ChatApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastucture.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  { }

    public DbSet<UserModel> User { get; set; }
    public DbSet<RoomMemberModel> RoomMembers { get; set; }
    public DbSet<MessageModel?> Messages { get; set; }
    public DbSet<RoomModel> Rooms { get; set; }
    public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.RoomMembers)
            .WithOne(gm => gm.User)
            .HasForeignKey(gm => gm.UserId);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.Messages)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserIdCreate);

        // Room Entity Configuration
        modelBuilder.Entity<RoomModel>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<RoomModel>()
            .HasMany(g => g.RoomMembers)
            .WithOne(gm => gm.Room)
            .HasForeignKey(gm => gm.RoomId);

        modelBuilder.Entity<RoomModel>()
            .HasMany(g => g.Messages)
            .WithOne(m => m.Room)
            .HasForeignKey(m => m.RoomId);

        modelBuilder.Entity<RoomModel>()
            .HasOne(g => g.Owner)
            .WithMany()
            .HasForeignKey(g => g.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
   

        // RoomMember Entity Configuration
        modelBuilder.Entity<RoomMemberModel>()
            .HasKey(gm => gm.Id);

        // Message Entity Configuration
        modelBuilder.Entity<MessageModel>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.RefreshTokens)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    } 
}