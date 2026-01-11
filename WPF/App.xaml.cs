using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Data;

namespace WPF
{
	public partial class App : Application
	{
		public static IHost AppHost { get; private set; }

		public App()
		{
			AppHost = Host.CreateDefaultBuilder()
				.ConfigureServices((context, services) =>
				{
					services.AddDbContext<QuizContext>(options =>
						options.UseSqlServer(
							"Server=(localdb)\\MSSQLLocalDB;Database=QuizDb;Trusted_Connection=True;"
						));

					services.AddTransient<MainWindow>();
				})
				.Build();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			AppHost.Start();
			var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
			mainWindow.Show();
			base.OnStartup(e);
		}
	}
}
