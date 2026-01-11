using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
	public class QuizContextFactory : IDesignTimeDbContextFactory<QuizContext>
	{
		public QuizContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<QuizContext>();

			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDb;Trusted_Connection=True;");

			return new QuizContext(optionsBuilder.Options);
		}
	}
}
