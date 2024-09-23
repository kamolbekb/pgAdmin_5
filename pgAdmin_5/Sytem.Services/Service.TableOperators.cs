using System.Data;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace PGAdminExamEdition;

public partial class Services
{
    public static void CreateTable(string query)
    {
        try
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query,connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table Created Successfully");
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while creating table: {ex.Message}");
        }
        
    }
    public static List<string> GetTableNames()
    {
        List<string> tables = new List<string>();
        try
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }
                }

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return tables;
    }
    public static void DeleteTable(string tableName)
    {
        string query = $"DROP TABLE "+tableName;
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(query,connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Deleted Successfully");
            }
            connection.Close();
        }
    }
    
    public static void DeleteTableCascade(string tableName)
    {
        string query = "TRUNCATE TABLE "+tableName;
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(query,connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Deleted Successfully");
            }
            connection.Close();
        }
    }
}