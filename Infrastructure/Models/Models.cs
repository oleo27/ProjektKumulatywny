using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Models.Interfaces;
using static Infrastructure.Models.Models;

namespace Infrastructure.Models
{
	public class Models
	{
		public class Question : Container<Answer>, IQuestion
		{
			public int Id { get; set; }
			public required string Text { get; set; }
			public int QuizId { get; set; }
			public Quiz Quiz { get; set; }

		}

		public class Answer : IAnswer
		{
			public int Id { get; set; }
			public required string Text { get; set; }
			public bool IsCorrect { get; set; }

			public int QuestionId { get; set; }
			public Question Question { get; set; }
		}

		public class Quiz : Container<Question>, IQuiz
		{
			public int Id { get; set; }
			public required string Title { get; set; }
		}
	}
}
