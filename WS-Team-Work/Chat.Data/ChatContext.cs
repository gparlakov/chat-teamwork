using Chat.Models;
using System.Data.Common;
using System.Data.Entity;

namespace Chat.Data
{
    public class ChatContext : DbContext
    {
        //public ChatContext()
        //    : base("SQLSERVER_CONNECTION_STRING")
        //{
        //}

        public ChatContext()
        {
            var connectionString = 
                System.Configuration.ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];

            this.Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Users
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Username).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(16);
            modelBuilder.Entity<User>().Property(x => x.Nickname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.SessionKey).HasMaxLength(50);

            //Messages
            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Message>().Property(x => x.Content).HasColumnType("text");

            base.OnModelCreating(modelBuilder);
        }
    }
}
