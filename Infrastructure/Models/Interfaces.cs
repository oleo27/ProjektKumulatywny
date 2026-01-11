using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
	public class Interfaces
	{
		public interface IQuestion
		{
			string Text { get; set; }
		}

		public interface IAnswer
		{
			string Text { get; set; }
			bool IsCorrect { get; set; }
		}

		public interface IQuiz
		{
			string Title { get; set; }
		}

		public class Container<T>
		{
			public List<T> Items { get; set; } = new List<T>();
			public void Add(T item)
			{
				Items.Add(item);
			}
		}
	}
}
