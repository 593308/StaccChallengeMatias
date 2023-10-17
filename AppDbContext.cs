using Microsoft.EntityFrameworkCore;
using StaccChallenge.Models;

namespace StaccChallenge
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Saving> Savings { get; set;}
        public DbSet<TotalSavings> TotalSavings { get; set; }
        public DbSet<Friendship> Friendships { get; set;}
        public DbSet<FriendRequest> FriendRequests { get; set;}
        public DbSet<Notification> Notifications { get; set;}
        public DbSet<Milestone> Milestones { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            // Configure the relationship between FriendRequest and User
            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Requester)
                .WithMany(u => u.SentRequests)
                .HasForeignKey(fr => fr.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Requested)
                .WithMany(u => u.ReceivedRequests)
                .HasForeignKey(fr => fr.RequestedId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Friendship and User
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User1)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User2)
                .WithMany() 
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=StaccChallenge;Trusted_connection=True;");
            }
        }


    }

    
}
