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
            RabbitConsumer rabbitConsumer = new RabbitConsumer();

            // List<Alert> RabbitListCENTER = await rabbitConsumer.ConsumeQueue("CENTER2");
            // List<Alert> RabbitListNORTH = await rabbitConsumer.ConsumeQueue("NORTH2");
            // List<Alert> RabbitListSOUTH = await rabbitConsumer.ConsumeQueue("SOUTH2");
            List<Alert> RabbitListOVERSEAS = await rabbitConsumer.ConsumeQueue("OVERSEAS2");


            // Console.WriteLine(RabbitListCENTER.Count());
            // Console.WriteLine(RabbitListNORTH.Count());
            // Console.WriteLine(RabbitListSOUTH.Count());
            Console.WriteLine(RabbitListOVERSEAS.Count());
            
            // foreach (Alert alert in RabbitListCENTER)
            // {
            //     AlertsListCENTER.Add(JsonSerializer.Deserialize<Alert>(rabbitMessage));
            // }

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
            dbContext.Database.EnsureCreated();

            foreach (Alert alert in RabbitListOVERSEAS)
            {
                try
                {
                    await dbContext.OVERSEASalerts.AddAsync(alert);
                    // await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("-------------------------------------------------------");
                }
            }
            // Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

            // foreach (Alert alert in RabbitListCENTER)
            // {
            //     try
            //     {
            //         await dbContext.CENTERalerts.AddAsync(alert);
            //         await dbContext.SaveChangesAsync();
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine(ex.Message);
            //         Console.WriteLine("-------------------------------------------------------");
            //     }
            // }
            // Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            // foreach (Alert alert in RabbitListSOUTH)
            // {
            //     try
            //     {
            //         await dbContext.SOUTHalerts.AddAsync(alert);
            //         await dbContext.SaveChangesAsync();
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine(ex.Message);
            //         Console.WriteLine("-------------------------------------------------------");
            //     }
            // }
            // Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            // foreach (Alert alert in RabbitListNORTH)
            // {
            //     try
            //     {
            //         await dbContext.NORTHalerts.AddAsync(alert);
            //         await dbContext.SaveChangesAsync();
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine(ex.Message);
            //         Console.WriteLine("-------------------------------------------------------");
            //     }
            // }
            
        }
    }
}
// System.InvalidOperationException
