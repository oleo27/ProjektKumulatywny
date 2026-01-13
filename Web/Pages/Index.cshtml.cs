using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Models.Models;

namespace Web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly QuizContext _db;
		public List<Quiz> Quizzes { get; set; } = [];

		public IndexModel(ILogger<IndexModel> logger, QuizContext db)
		{
			_logger = logger;
			_db = db;
		}

		public void OnGet()
		{
			Quizzes = _db.Quizzes.AsNoTracking().ToList();
		}
	}
}
