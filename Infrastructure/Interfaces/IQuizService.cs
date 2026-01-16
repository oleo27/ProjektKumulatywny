using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IQuizService<T> where T : class
{
	Task<List<T>> GetAllAsync();
	Task<T?> GetAsync(int id);
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}
