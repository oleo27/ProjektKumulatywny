internal class Program
{
	interface IQuestion
	{
		string Text { get; set; }
	}

	interface IAnswer
	{
		string Text { get; set; }
		bool IsCorrect { get; set; }
	}

	interface IQuiz
	{
		string Title { get; set; }
	}

	class Container<T>
	{
		public List<T> Items { get; set; } = new List<T>();
		public void Add(T item)
		{
			Items.Add(item);
		}
	}

	class Question: Container<Answer>, IQuestion
	{
		public string Text { get; set; }
		public Question(string text)
		{
			Text = text;
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

	class Quiz : Container<Question>, IQuiz
	{
		public string Title { get; set; }
		public Quiz(string title)
		{
			Title = title;
		}
	}
	private static void Main(string[] args)
	{

	}
}