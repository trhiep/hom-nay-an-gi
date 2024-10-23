using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomNayAnGiAPI.Models
{
    public partial class HomNayAnGiContext : DbContext
    {
        public HomNayAnGiContext()
        {
        }

        public HomNayAnGiContext(DbContextOptions<HomNayAnGiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<NutritionFact> NutritionFacts { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; } = null!;
        public virtual DbSet<RecipeComment> RecipeComments { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
        public virtual DbSet<StepImage> StepImages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=HomNayAnGi;User ID=sa;Password=12345678;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientName).HasMaxLength(255);
            });

            modelBuilder.Entity<NutritionFact>(entity =>
            {
                entity.HasKey(e => e.RecipeId)
                    .HasName("NutritionFact_pk");

                entity.ToTable("NutritionFact");

                entity.Property(e => e.RecipeId).ValueGeneratedNever();

                entity.HasOne(d => d.Recipe)
                    .WithOne(p => p.NutritionFact)
                    .HasForeignKey<NutritionFact>(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_5");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DifficultyLevel).HasMaxLength(10);

                entity.Property(e => e.RecipeName).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Recipe_RecipeCategory");
            });

            modelBuilder.Entity<RecipeCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("RecipeCategory_pk");

                entity.ToTable("RecipeCategory");

                entity.Property(e => e.CategoryName).HasMaxLength(255);
            });

            modelBuilder.Entity<RecipeComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("RecipeComment_pk");

                entity.ToTable("RecipeComment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeComments)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK_8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RecipeComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_9");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId })
                    .HasName("RecipeIngredient_pk");

                entity.ToTable("RecipeIngredient");

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_3");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_2");
            });

            modelBuilder.Entity<RecipeStep>(entity =>
            {
                entity.HasKey(e => e.StepId)
                    .HasName("RecipeStep_pk");

                entity.ToTable("RecipeStep");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeSteps)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK_4");
            });

            modelBuilder.Entity<StepImage>(entity =>
            {
                entity.ToTable("StepImage");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.StepImages)
                    .HasForeignKey(d => d.StepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StepImage_RecipeStep");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.HasMany(d => d.Recipes)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserFavorite",
                        l => l.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_7"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_6"),
                        j =>
                        {
                            j.HasKey("UserId", "RecipeId").HasName("UserFavorite_pk");

                            j.ToTable("UserFavorite");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
