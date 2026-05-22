using Microsoft.Extensions.DependencyInjection;
using PDV.Application.Interfaces;
using PDV.Infrastructure.Database;
using PDV.Infrastructure.Repositories.Produtos;

namespace PDV.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddSingleton<SqliteConnectionFactory>();
            services.AddScoped<IProdutoVariacaoRepository, ProdutoVariacaoRepository>();

            services.AddTransient<FrmFrenteCaixaPDV>();

            var provider = services.BuildServiceProvider();

            global::System.Windows.Forms.Application.Run(
                provider.GetRequiredService<FrmFrenteCaixaPDV>()
            );
        }
    }
}