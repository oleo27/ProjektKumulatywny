internal class Program
{
	class Question
	{
		public string Text { get; set; }
		public List<Answer> Answers { get; set; }
		public Question(string text)
		{
			Text = text;
			Answers = new List<Answer>();
		}
		public void addAnswers(Answer answer)
		{
			Answers.Add(answer);
		}

	}

	class Answer
	{
		public string Text { get; set; }
		public bool IsCorrect { get; set; }
		public Answer(string text, bool isCorrect)
		{
			Text = text;
			IsCorrect = isCorrect;
		}
	}

	class Quiz
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