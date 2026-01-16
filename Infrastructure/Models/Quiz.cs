using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
	public class Quiz
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public List<Question> Items { get; set; } = new();
	}
}
