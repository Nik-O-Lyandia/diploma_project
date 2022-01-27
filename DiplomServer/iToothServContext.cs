using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiplomServer
{
    public partial class iToothServContext : DbContext
    {
        public iToothServContext()
        {
        }

        public iToothServContext(DbContextOptions<iToothServContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Pacient> Pacients { get; set; }
        public virtual DbSet<PacientDoctor> PacientDoctors { get; set; }
        public virtual DbSet<Reseption> Reseptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=iToothServ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issueDate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PacientId).HasColumnName("pacient_id");

                entity.Property(e => e.Paied)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("paied")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bill_doctor_fk");

                entity.HasOne(d => d.Pacient)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bill_pacient_fk");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentText)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("commentText");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PacientId).HasColumnName("pacient_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_doctor_fk");

                entity.HasOne(d => d.Pacient)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_pacient_fk");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DocFoto)
                    .HasColumnType("image")
                    .HasColumnName("docFoto");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Passportnumber).HasColumnName("passportnumber");

                entity.Property(e => e.Passportseries)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("passportseries")
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("note");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NoteDate)
                    .HasColumnType("date")
                    .HasColumnName("note_date");

                entity.Property(e => e.NoteText)
                    .HasColumnType("text")
                    .HasColumnName("note_text");

                entity.Property(e => e.PacientId).HasColumnName("pacient_id");

                entity.HasOne(d => d.Pacient)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("note_pacient_fk");
            });

            modelBuilder.Entity<Pacient>(entity =>
            {
                entity.ToTable("pacient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Passportnumber).HasColumnName("passportnumber");

                entity.Property(e => e.Passportseries)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("passportseries")
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<PacientDoctor>(entity =>
            {
                entity.ToTable("pacient_doctor");

                entity.Property(e => e.PacientDoctorId).HasColumnName("pacient_doctor_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PacientId).HasColumnName("pacient_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PacientDoctors)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pacient_doctor_doctor_fk");

                entity.HasOne(d => d.Pacient)
                    .WithMany(p => p.PacientDoctors)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pacient_doctor_pacient_fk");
            });

            modelBuilder.Entity<Reseption>(entity =>
            {
                entity.ToTable("reseption");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attendingdate)
                    .HasColumnType("date")
                    .HasColumnName("attendingdate");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PacientId).HasColumnName("pacient_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("time")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Reseptions)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reseption_doctor_fk");

                entity.HasOne(d => d.Pacient)
                    .WithMany(p => p.Reseptions)
                    .HasForeignKey(d => d.PacientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reseption_pacient_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
