using System.Configuration;
using System.Data;
using System.Windows;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		//public static IHost? AppHost { get; private set; }

		//public App()
		//{
		//	AppHost = Host.CreateDefaultBuilder()
		//		.ConfigureServices((context, services) =>
		//		{
		//			// Dodaj DbContext
		//			services.AddDbContext<QuizContext>(options =>
		//				options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDb;Trusted_Connection=True;"));

		//			// Dodaj główne okno
		//			services.AddTransient<MainWindow>();
		//		})
		//		.Build();
		//}

		//protected override void OnStartup(StartupEventArgs e)
		//{
		//	AppHost.Start();
		//	var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
		//	mainWindow.Show();
		//	base.OnStartup(e);
		//}
	}

}
