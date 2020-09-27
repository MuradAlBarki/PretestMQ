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

                entity.Property(e => e.DashboardId).HasColumnName("dashboard_id");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.OrgId).HasColumnName("org_id");

                entity.Property(e => e.PanelId).HasColumnName("panel_id");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnName("request")
                    .IsUnicode(false);

                entity.Property(e => e.RuleId).HasColumnName("rule_id");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("request");

                entity.Property(e => e.DashboardId).HasColumnName("dashboard_id");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.OrgId).HasColumnName("org_id");

                entity.Property(e => e.PanelId).HasColumnName("panel_id");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.RuleId).HasColumnName("rule_id");

                entity.Property(e => e.RuleName)
                    .IsRequired()
                    .HasColumnName("rule_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RuleUrl)
                    .IsRequired()
                    .HasColumnName("rule_url")
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
