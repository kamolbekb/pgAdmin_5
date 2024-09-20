using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace PGAdminExamEdition;

public class Program
{
    public static void Main(string[] args)
    {
        
        bool answer = true;
        List<string> selectManu = new List<string>
        {
            "Connection to DB",
            "Exit"
        };
        while (answer)
        {
            int choise= Services.Selector(selectManu);
            Console.Clear();
            
            switch (choise)
            {
                case 0:
                    Console.WriteLine("Host(localhost):");
                    string host = Console.ReadLine();
                    Console.WriteLine("Port(5432):");
                    string port =Console.ReadLine();
                    Console.WriteLine("Database(name of your DB)");
                    string db=Console.ReadLine();
                    Console.WriteLine("User Id(postgres):");
                    string userId=Console.ReadLine();
                    Console.WriteLine( "Password(YourPass)");
                    string dbPass = Console.ReadLine();
                    Services.ConnectionString =
                        $"Host={host};Port={port};Database={db}; User Id= {userId}; Password = {dbPass};";
                    if (Services.CheckConnection())
                    {
                        while (true)
                        {
                            List<string> selectConnected = new List<string>
                            {
                                "Aggregates",
                                "Collations",
                                "Domains",
                                "FTS Configurations",
                                "FTS Dictionaries",
                                "FTS Parsers",
                                "FTS Templates",
                                "Foreign Tables",
                                "Functions",
                                "Materialized views",
                                "Operators",
                                "Procedures",
                                "Sequences",
                                "Tables",
                                "Trigger Functions",
                                "Types",
                                "Views"
                            };
                            Console.WriteLine("1.Tables\n0.Exit");
                            int choiseConnection = Services.Selector(selectConnected);
                            switch (choiseConnection)
                            {
                                case 0:
                                    Console.WriteLine("1.Show tables\n2.Add new Table\n3.Update existing table");
                                    break;
                                case 1:
                                    return;
                                default:
                                    Console.WriteLine("No similar action");
                                    break;
                            }
                        }
                    }

                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 1:
                    Console.WriteLine("Thank you for using our app!");
                    return;
                default:
                    Console.WriteLine("Unexpected answer, please try again");
                    Console.WriteLine("Press any button to continue!");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }
    
}