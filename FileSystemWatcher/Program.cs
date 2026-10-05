using System;
using System.IO;

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

        }
    }
}
