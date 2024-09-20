using System.Data;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace PGAdminExamEdition;
public class Services
{
    public static string ConnectionString { get; set; }
    public static bool CheckConnection()
    {
        try
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                Console.Clear();
                Console.WriteLine("Connected successfully!");
                connection.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}!");
            return false;
        }
    }

    public static void GetTableNames()
    {
        try
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                var schema = connection.GetSchema("Tables");

                if (schema.Rows.Count == 0)
                {
                    Console.WriteLine("There are no Tables inside of this DB yet!");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    foreach (DataRow row in schema.Rows)
                    {
                        Console.Write(row["table_name"]);
                    }
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void UpdateTable(string query)
    {
    }

    public static void GetSchemas()
    {
        try
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var schema = connection.GetSchema("Schemas");

                if (schema.Rows.Count == 0)
                {
                    Console.WriteLine("There are no schemas inside this DB yet!");
                }
                else
                {
                    Console.WriteLine("Schemas found:");
                    foreach (DataRow sch in schema.Rows)
                    {
                        Console.WriteLine(sch["shcema_name"]);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public static int Selector(List<string> choise)
    {
        int indexSelection = 0;
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < choise.Count; i++)
            {
                if (i == indexSelection)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Beep();
                    Console.WriteLine(choise[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{choise[i]}");
                    Console.ResetColor();
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.DownArrow) 
                indexSelection = (indexSelection + 1) % choise.Count;
            else if (key.Key == ConsoleKey.UpArrow)
                indexSelection = (indexSelection - 1 + choise.Count)%choise.Count;
            else if (key.Key == ConsoleKey.Enter) 
                return indexSelection;
        }
    }
}