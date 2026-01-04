using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using static Infrastructure.Data;

namespace Infrastructure
{
	public class QuizContextFactory : IDesignTimeDbContextFactory<QuizContext>
	{
		public QuizContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<QuizContext>();

			// Podaj swój connection string tutaj
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDb;Trusted_Connection=True;");

			return new QuizContext(optionsBuilder.Options);
		}
	}
}
