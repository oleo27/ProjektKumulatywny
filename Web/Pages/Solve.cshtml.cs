using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using static Infrastructure.Models.Models;

public class SolveModel : PageModel
{
	private readonly QuizContext _db;

	public Quiz Quiz { get; set; }

	[BindProperty]
	public List<UserAnswer> Answers { get; set; } = [];

	public int Score { get; set; }
	public bool Finished { get; set; }

	public SolveModel(QuizContext db)
	{
		_db = db;
	}

	public void OnGet(int quizId)
	{
		Quiz = _db.Quizzes
			.Include(q => q.Items)
				.ThenInclude(q => q.Items)
			.First(q => q.Id == quizId);

		Answers = Quiz.Items
			.Select(q => new UserAnswer { QuestionId = q.Id })
			.ToList();
	}

	public void OnPost(int quizId)
	{
		Quiz = _db.Quizzes
			.Include(q => q.Items)
				.ThenInclude(q => q.Items)
			.First(q => q.Id == quizId);

		Score = 0;

		foreach (var question in Quiz.Items)
		{
			var userAnswer = Answers
				.FirstOrDefault(a => a.QuestionId == question.Id);

			if (userAnswer == null) continue;

			var correct = question.Items
				.FirstOrDefault(a => a.IsCorrect);

			if (correct != null && correct.Id == userAnswer.SelectedAnswerId)
				Score++;
		}

		Finished = true;
	}
}

