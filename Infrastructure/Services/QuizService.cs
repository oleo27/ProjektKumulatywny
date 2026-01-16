using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuizService<T> : IQuizService<T> where T : class
{
	private readonly QuizContext _context;
	private readonly DbSet<T> _set;

	public QuizService(QuizContext context)
	{
		_context = context;
		_set = context.Set<T>();
	}

	public async Task<List<T>> GetAllAsync()
		=> await _set.AsNoTracking().ToListAsync();

	public async Task<T?> GetAsync(int id)
		=> await _set.FindAsync(id);

	public async Task AddAsync(T entity)
	{
		await _set.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(T entity)
	{
		_set.Update(entity);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(T entity)
	{
		_set.Remove(entity);
		await _context.SaveChangesAsync();
	}
}


