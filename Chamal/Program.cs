using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MyNamespace
{
    class MyClassCS
    {
        static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:false)
                .Build();

            var service = new ServiceCollection();
            
            service.AddDbContext<CommandsDbContext>(options =>
                options.UseMySql(config["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(config["ConnectionStrings:DefaultConnection"])));

            var provider = service.BuildServiceProvider();

            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommandsDbContext>();
            var fromSql = dbContext.Alert;

            foreach (Alert alert in fromSql)
            {
                TimeSpan.FromSeconds(30);
                alert.status = "DONE";
                dbContext.Alert.Where(a => a.alert_id == alert.alert_id).Select(a => new{a.status, a.lon});
            }
        }
    }
}


