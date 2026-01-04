using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Core.Models;

namespace Core
{
	internal class Data
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
		}
	}
}
