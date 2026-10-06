using System;
using System.IO;
using System.Text.Json;

namespace MyNamespace
{
    class MyClassCS
    {
        static void Main()
        {
            FileWatcher fileWatcher = new FileWatcher();
            fileWatcher.Watch();
        
            // System.Console.WriteLine("-----------");
            // foreach (string file in Directory.EnumerateFiles(@"C:\Users\User\Downloads\alert-simulator\alert-simulator\alerts", "*.json"))
            // {
            //     string contents = File.ReadAllText(file);
            //     System.Console.WriteLine(contents);
            // }
            string[] files;

            files = Directory.GetFiles(@"C:\Users\User\Downloads\alert-simulator\alert-simulator\alerts", @"*.json", SearchOption.AllDirectories);

            AlertProduce alertProduce = new AlertProduce();
            
            foreach (string file in files)
            {
                // var c = JsonSerializer.Deserialize<Alert>(File.ReadAllText(file));
                alertProduce.Produce("ALERTS", File.ReadAllText(file));
            }

            alertProduce.Dispose();
        }
    }
}
