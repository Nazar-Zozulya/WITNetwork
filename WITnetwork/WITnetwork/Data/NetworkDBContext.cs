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

    public DbSet<Album> Albums { get; set; }

    public DbSet<AlbumImage> AlbumImages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        // CHAT

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



        // FRIENDSHIP
        
        builder.Entity<Friendship>()
            .HasOne(f => f.From)
            .WithMany(u => u.FriendshipsFrom)
            .HasForeignKey(f => f.FromId);

        builder.Entity<Friendship>()
            .HasOne(f => f.To)
            .WithMany(u => u.FriendshipsTo)
            .HasForeignKey(f => f.ToId);



        // ALBUM

        builder.Entity<Album>()
            .HasOne(a => a.Profile)
            .WithMany(p => p.Albums)
            .HasForeignKey(a => a.ProfileId);
        
        builder.Entity<AlbumImage>()
            .HasOne(ai => ai.Album)
            .WithMany(a => a.Images)
            .HasForeignKey(ai => ai.AlbumId);

        

        // USER

        builder.Entity<Profile>()
            .HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<Profile>(p => p.UserId);
    }
}
