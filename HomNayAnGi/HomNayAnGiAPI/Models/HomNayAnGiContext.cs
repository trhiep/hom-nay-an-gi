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
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<NutritionFact> NutritionFacts { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; } = null!;
        public virtual DbSet<RecipeComment> RecipeComments { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<RecipeMeal> RecipeMeals { get; set; } = null!;
        public virtual DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
        public virtual DbSet<SignupOtp> SignupOtps { get; set; } = null!;
        public virtual DbSet<StepImage> StepImages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserFavorite> UserFavorites { get; set; } = null!;
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().
                    SetBasePath(Directory.GetCurrentDirectory()).
                    AddJsonFile("appsettings.json", optional: false);
                IConfiguration con = builder.Build();
                optionsBuilder.UseSqlServer(con.GetConnectionString("DBContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientName).HasMaxLength(255);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("Ingredient_User");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meal");

                entity.Property(e => e.MealName).HasMaxLength(100);
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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RecipeCategories)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("RecipeCategory_User");
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

            modelBuilder.Entity<RecipeMeal>(entity =>
            {
                entity.ToTable("RecipeMeal");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.RecipeMeals)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RecipeMeal_Meal");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeMeals)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RecipeMeal_Recipe");
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

            modelBuilder.Entity<SignupOtp>(entity =>
            {
                entity.HasKey(e => e.SignupRequestId)
                    .HasName("SignupOTP_pk");

                entity.ToTable("SignupOTP");

                entity.Property(e => e.SignupRequestId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiresAt).HasColumnType("datetime");

                entity.Property(e => e.Otpstring)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OTPString");
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
            });

            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.ToTable("UserFavorite");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.UserFavorites)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFavorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_6");
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.ToTable("UserRefreshToken");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.ExpiresAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRefreshToken_User_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
