using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace WpfApp
{
	public partial class MainWindow : Window
	{
		private readonly IQuizService<Quiz> _quizService;
		private readonly IQuizService<Question> _questionService;
		private readonly IQuizService<Answer> _answerService;

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

			var context = new QuizContext(options);

			_quizService = new QuizService<Quiz>(context);
			_questionService = new QuizService<Question>(context);
			_answerService = new QuizService<Answer>(context);

			QuizListBox.ItemsSource = _quizzes;
			QuestionListBox.ItemsSource = _questions;
			AnswerListBox.ItemsSource = _answers;

			QuizListBox.DisplayMemberPath = "Title";
			QuestionListBox.DisplayMemberPath = "Text";
			AnswerListBox.DisplayMemberPath = "Text";

			Loaded += async (_, _) => await LoadQuizzesAsync();
		}

		private async Task LoadQuizzesAsync()
		{
			_allQuizzes.Clear();
			_quizzes.Clear();

			var quizzes = await _quizService.GetAllAsync();
			foreach (var q in quizzes)
			{
				_allQuizzes.Add(q);
				_quizzes.Add(q);
			}

			_questions.Clear();
			_answers.Clear();
		}

		private async Task LoadQuestionsAsync(int quizId)
		{
			_questions.Clear();
			var questions = await _questionService.GetAllAsync();
			foreach (var q in questions.Where(x => x.QuizId == quizId))
				_questions.Add(q);

			_answers.Clear();
		}

		private async Task LoadAnswersAsync(int questionId)
		{
			_answers.Clear();
			var answers = await _answerService.GetAllAsync();
			foreach (var a in answers.Where(x => x.QuestionId == questionId))
				_answers.Add(a);
		}

		private void QuizFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var text = QuizFilterTextBox.Text.ToLower();
			_quizzes.Clear();
			foreach (var quiz in _allQuizzes.Where(q => q.Title.ToLower().Contains(text)))
				_quizzes.Add(quiz);
		}

		private async void AddQuiz_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(QuizTitleTextBox.Text)) return;

			await _quizService.AddAsync(new Quiz { Title = QuizTitleTextBox.Text });
			await LoadQuizzesAsync();

			QuizTitleTextBox.Clear();
		}

		private async void EditQuiz_Click(object sender, RoutedEventArgs e)
		{
			if (QuizListBox.SelectedItem is not Quiz selected) return;

			selected.Title = QuizTitleTextBox.Text;
			await _quizService.UpdateAsync(selected);
			await LoadQuizzesAsync();
		}

		private async void DeleteQuiz_Click(object sender, RoutedEventArgs e)
		{
			if (QuizListBox.SelectedItem is not Quiz selected) return;

			await _quizService.DeleteAsync(selected);
			await LoadQuizzesAsync();
		}

		private async void QuizListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (QuizListBox.SelectedItem is Quiz quiz)
				await LoadQuestionsAsync(quiz.Id);
		}

		private async void QuestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is Question question)
				await LoadAnswersAsync(question.Id);
		}

		private async void AddQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (QuizListBox.SelectedItem is not Quiz quiz || string.IsNullOrWhiteSpace(QuestionTextBox.Text)) return;

			await _questionService.AddAsync(new Question
			{
				Text = QuestionTextBox.Text,
				QuizId = quiz.Id
			});

			await LoadQuestionsAsync(quiz.Id);
			QuestionTextBox.Clear();
		}

		private async void EditQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is not Question q) return;

			q.Text = QuestionTextBox.Text;
			await _questionService.UpdateAsync(q);
			await LoadQuestionsAsync(q.QuizId);
		}

		private async void DeleteQuestion_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is not Question q) return;

			await _questionService.DeleteAsync(q);
			await LoadQuestionsAsync(q.QuizId);
		}

		private async void AddAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (QuestionListBox.SelectedItem is not Question q || string.IsNullOrWhiteSpace(AnswerTextBox.Text)) return;

			await _answerService.AddAsync(new Answer
			{
				Text = AnswerTextBox.Text,
				IsCorrect = IsCorrectCheckBox.IsChecked ?? false,
				QuestionId = q.Id
			});

			await LoadAnswersAsync(q.Id);
			AnswerTextBox.Clear();
			IsCorrectCheckBox.IsChecked = false;
		}

		private async void EditAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (AnswerListBox.SelectedItem is not Answer a) return;

			a.Text = AnswerTextBox.Text;
			a.IsCorrect = IsCorrectCheckBox.IsChecked ?? false;

			await _answerService.UpdateAsync(a);
			await LoadAnswersAsync(a.QuestionId);
		}

		private async void DeleteAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (AnswerListBox.SelectedItem is not Answer a) return;

			await _answerService.DeleteAsync(a);
			await LoadAnswersAsync(a.QuestionId);
		}
	}
}
