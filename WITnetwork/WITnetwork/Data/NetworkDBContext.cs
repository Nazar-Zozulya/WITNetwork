using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Models;

namespace WITnetwork.Data;

public class NetworkDBContext(DbContextOptions<NetworkDBContext> options): IdentityDbContext<UserProfile,IdentityRole<long>, long> (options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Image> Images { get; set; }

    public DbSet<Friendship> Friendships { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<PostImage> PostImages { get; set; }

    public DbSet<EmailVerification> EmailVerifications { get; set; }

    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<Chat>()
            .HasMany(c => c.Users)
            .WithMany() 
            .UsingEntity(j => j.ToTable("ChatParticipants")); 

        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            
            .OnDelete(DeleteBehavior.SetNull); 
            
        builder.Entity<Message>()
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
