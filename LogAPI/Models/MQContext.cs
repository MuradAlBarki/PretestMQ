using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LogAPI.Models
{
    public partial class MQContext : DbContext
    {
        public MQContext()
        {
        }

        public MQContext(DbContextOptions<MQContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Request> Request { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnName("request")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
