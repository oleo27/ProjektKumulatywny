internal class Program
{
	interface IQuestion
	{
		string Text { get; set; }
		List<Answer> Answers { get; set; }
		public void addAnswer(Answer answer);
	}

	interface IAnswer
	{
		string Text { get; set; }
		bool IsCorrect { get; set; }
	}

	interface IQuiz
	{
		string Title { get; set; }
		List<Question> Questions { get; set; }
		public void addQuestion(Question question);
	}

	class Question: IQuestion 
	{
		public string Text { get; set; }
		public List<Answer> Answers { get; set; }
		public Question(string text)
		{
			Text = text;
			Answers = new List<Answer>();
		}
		public void addAnswer(Answer answer)
		{
			Answers.Add(answer);
		}

	}

	class Answer : IAnswer
	{
		public string Text { get; set; }
		public bool IsCorrect { get; set; }
		public Answer(string text, bool isCorrect)
		{
			Text = text;
			IsCorrect = isCorrect;
		}
	}

	class Quiz : IQuiz
	{
		public string Title { get; set; }
		public List<Question> Questions { get; set; }
		public Quiz(string title)
		{
			Title = title;
		}

		public void addQuestion(Question question)
		{
			Questions.Add(question);
		}
	}
	private static void Main(string[] args)
	{
		
	}
}