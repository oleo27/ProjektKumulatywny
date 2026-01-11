using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Models.Models;

namespace WpfApp
{
	public partial class MainWindow : Window
	{
		private readonly QuizContext _db;

		private readonly ObservableCollection<Quiz> _allQuizzes = new();
		private readonly ObservableCollection<Quiz> _quizzes = new();
		private readonly ObservableCollection<Question> _questions = new();
		private readonly ObservableCollection<Answer> _answers = new();

		public MainWindow()
		{
			InitializeComponent();

			var options = new DbContextOptionsBuilder<QuizContext>()
				.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDb;Trusted_Connection=True;")
				.Options;

			_db = new QuizContext(options);

			QuizListBox.ItemsSource = _quizzes;
			QuestionListBox.ItemsSource = _questions;
			AnswerListBox.ItemsSource = _answers;

			QuizListBox.DisplayMemberPath = "Title";
			QuestionListBox.DisplayMemberPath = "Text";
			AnswerListBox.DisplayMemberPath = "Text";

			LoadQuizzes();
		}

		private void LoadQuizzes()
		{
			_allQuizzes.Clear();
			_quizzes.Clear();

			foreach (var quiz in _db.Quizzes.AsNoTracking().ToList())
			{
				_allQuizzes.Add(quiz);
				_quizzes.Add(quiz);
			}

			_questions.Clear();
			_answers.Clear();
		}

		private void LoadQuestions(int quizId)
		{
			_questions.Clear();
			foreach (var q in _db.Questions
				.Where(x => x.QuizId == quizId)
				.AsNoTracking()
				.ToList())
			{
				_questions.Add(q);
			}

			_answers.Clear();
		}

		private void LoadAnswers(int questionId)
		{
			_answers.Clear();
			foreach (var a in _db.Answers
				.Where(x => x.QuestionId == questionId)
				.AsNoTracking()
				.ToList())
			{
				_answers.Add(a);
			}
		}

		private void ApplyQuizFilter(string text)
		{
			text = text.ToLower();

			_quizzes.Clear();

			foreach (var quiz in _allQuizzes
				.Where(q => q.Title.ToLower().Contains(text)))
			{
				_quizzes.Add(quiz);
			}
		}

		private void QuizFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			ApplyQuizFilter(((TextBox)sender).Text);
		}

		private void AddQuiz_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(QuizTitleTextBox.Text)) return;

			_db.Quizzes.Add(new Quiz { Title = QuizTitleTextBox.Text });
			_db.SaveChanges();

			LoadQuizzes();
			QuizTitleTextBox.Clear();
		}

		private void EditQuiz_Click(object sender, RoutedEventArgs e)
		{
			if (QuizListBox.SelectedItem is not Quiz selected) return;

			var quiz = _db.Quizzes.Find(selected.Id);
			if (quiz == null) return;

			quiz.Title = QuizTitleTextBox.Text;
			_db.SaveChanges();

			LoadQuizzes();
		}

		private void DeleteQuiz_Click(object sender, RoutedEventArgs e)
		{
			if (QuizListBox.SelectedItem is not Quiz selected) return;

			var quiz = _db.Quizzes.Find(selected.Id);
			if (quiz == null) return;

			_db.Quizzes.Remove(quiz);
			_db.SaveChanges();

			LoadQuizzes();
		}

		private void QuizListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (QuizListBox.SelectedItem is Quiz quiz)
				LoadQuestions(quiz.Id);
		}

		private void AddQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (QuizListBox.SelectedItem is not Quiz quiz ||
				string.IsNullOrWhiteSpace(QuestionTextBox.Text)) return;

			_db.Questions.Add(new Question
			{
				Text = QuestionTextBox.Text,
				QuizId = quiz.Id
			});

			_db.SaveChanges();
			LoadQuestions(quiz.Id);
			QuestionTextBox.Clear();
		}

		private void EditQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is not Question selected) return;

			var q = _db.Questions.Find(selected.Id);
			if (q == null) return;

			q.Text = QuestionTextBox.Text;
			_db.SaveChanges();

			LoadQuestions(q.QuizId);
		}

		private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is not Question selected) return;

			var q = _db.Questions.Find(selected.Id);
			if (q == null) return;

			_db.Questions.Remove(q);
			_db.SaveChanges();

			LoadQuestions(q.QuizId);
		}

		private void QuestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is Question q)
				LoadAnswers(q.Id);
		}

		private void AddAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is not Question q ||
				string.IsNullOrWhiteSpace(AnswerTextBox.Text)) return;

			_db.Answers.Add(new Answer
			{
				Text = AnswerTextBox.Text,
				IsCorrect = IsCorrectCheckBox.IsChecked ?? false,
				QuestionId = q.Id
			});

			_db.SaveChanges();
			LoadAnswers(q.Id);

			AnswerTextBox.Clear();
			IsCorrectCheckBox.IsChecked = false;
		}

		private void EditAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (AnswerListBox.SelectedItem is not Answer selected) return;

			var a = _db.Answers.Find(selected.Id);
			if (a == null) return;

			a.Text = AnswerTextBox.Text;
			a.IsCorrect = IsCorrectCheckBox.IsChecked ?? false;

			_db.SaveChanges();
			LoadAnswers(a.QuestionId);
		}

		private void DeleteAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (AnswerListBox.SelectedItem is not Answer selected) return;

			var a = _db.Answers.Find(selected.Id);
			if (a == null) return;

			_db.Answers.Remove(a);
			_db.SaveChanges();

			LoadAnswers(a.QuestionId);
		}
	}
}
