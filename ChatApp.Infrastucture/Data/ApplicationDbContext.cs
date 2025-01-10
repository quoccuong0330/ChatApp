using ChatApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastucture.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  { }

    public DbSet<UserModel> User { get; set; }
    public DbSet<ChatRoomModel> ChatRoom { get; set; }
    public DbSet<MessageModel> Message { get; set; }
    public DbSet<SeenMessageModel> SeenMessage { get; set; }
    public DbSet<UserChatRoomModel> UserChatRoom { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserChatRoomModel>()
            .HasKey(uc => new { uc.UserId, uc.RoomId }); // Composite Key

        modelBuilder.Entity<UserChatRoomModel>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserChatRooms)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserChatRoomModel>()
            .HasOne(uc => uc.ChatRoom)
            .WithMany(c => c.UserChatRooms)
            .HasForeignKey(uc => uc.RoomId);

        modelBuilder.Entity<UserChatRoomModel>()
            .Property(uc => uc.JoinedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<MessageModel>()
            .HasOne(m => m.Sender)
            .WithMany(m => m.Messages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);;

        modelBuilder.Entity<MessageModel>()
            .HasOne(m => m.Room)
            .WithMany(m => m.Messages)
            .HasForeignKey(m => m.RoomId)
            .OnDelete(DeleteBehavior.Restrict);;
        
        modelBuilder.Entity<SeenMessageModel>()
            .HasKey(sm => new { sm.RoomId, sm.MessageId, sm.UserId }); // Composite Primary Key

        modelBuilder.Entity<SeenMessageModel>()
            .HasOne(sm => sm.Room)
            .WithMany(r => r.SeenMessages) // Một Room có nhiều SeenMessage
            .HasForeignKey(sm => sm.RoomId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SeenMessageModel>()
            .HasOne(sm => sm.Message)
            .WithMany(m => m.SeenMessages) // Một Message có nhiều SeenMessage
            .HasForeignKey(sm => sm.MessageId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SeenMessageModel>()
            .HasOne(sm => sm.User)
            .WithMany(u => u.SeenMessages) // Một User có nhiều SeenMessage
            .HasForeignKey(sm => sm.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ChatRoomModel>()
            .HasOne(c => c.LastMessage)
            .WithOne()
            .HasForeignKey<ChatRoomModel>(c => c.LastMessageId);
    } 
}