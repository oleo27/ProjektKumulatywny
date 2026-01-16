using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Data
{
		public class QuizContext : DbContext
		{
			public DbSet<Quiz> Quizzes { get; set; }
			public DbSet<Question> Questions { get; set; }
			public DbSet<Answer> Answers { get; set; }

			public QuizContext(DbContextOptions<QuizContext> options)
				: base(options) { }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<Quiz>()
					.HasMany(q => q.Items)
					.WithOne(q => q.Quiz)
					.HasForeignKey(q => q.QuizId)
					.OnDelete(DeleteBehavior.Cascade);

				modelBuilder.Entity<Question>()
					.HasMany(q => q.Items)
					.WithOne(a => a.Question)
					.HasForeignKey(a => a.QuestionId)
					.OnDelete(DeleteBehavior.Cascade);
			}

			protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			{
				if (!optionsBuilder.IsConfigured)
				{
					optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDb;Trusted_Connection=True;");
				}
			}
		}
}
