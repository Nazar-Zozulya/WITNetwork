using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Models;

namespace WITnetwork.Data;

public class NetworkDBContext(DbContextOptions<NetworkDBContext> options): IdentityDbContext<UserProfile,IdentityRole<Guid>, Guid> (options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Image> Images { get; set; }

    public DbSet<Friendship> Friendships { get; set; }

    public DbSet<Chat> Chats { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<PostImage> PostImages { get; set; }
}
