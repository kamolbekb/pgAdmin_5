using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
namespace PGAdminExamEdition;
"ConnectionStrings": {
    "StudentString": "Host=::1;Port=5432;Database=student; User Id= postgres; Password = 2004Kama;"
}
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
             int choise = Services.Selector(selectManu);
             Console.Clear();

             switch (choise)
             {
                 case 0:
                     Console.WriteLine("Host(localhost):");
                     string host = Console.ReadLine();
                     Console.WriteLine("Port(5432):");
                     string port = Console.ReadLine();
                     Console.WriteLine("User Id(postgres):");
                     string userId = Console.ReadLine();
                     Console.WriteLine("Password(YourPass)");
                     string dbPass = Console.ReadLine();
                     string connectionStringReg =
                         $"Host = {host};Port = {port};User Id = {userId}; Password = {dbPass}; Database = postgres;";
                     List<string> databases = Services.GetDatabases(connectionStringReg);

                     if (databases.Count > 0)
                     {
                         databases.Add("Create a new database...");
                         int selectedIndex = Services.Selector(databases);
                         
                         if (selectedIndex == databases.Count - 1)
                         {
                             Console.Clear();
                             Services.CreateDatabase(connectionStringReg);
                             databases = Services.GetDatabases(connectionStringReg);
                             string chosenDatabase = databases[selectedIndex];
                             
                             Services.ConnectionString=$"Host={host};Username={userId};Password={dbPass};Port={port};Database={chosenDatabase}";
                             Services.ConnectToDatabase();
                             Console.ReadKey();
                             
                         }
                         else
                         {
                             Console.Clear();
                             string chosenDatabase = databases[selectedIndex];
                             
                             Services.ConnectionString=$"Host={host};Username={userId};Password={dbPass};Port={port};Database={chosenDatabase}";
                             Services.ConnectToDatabase();
                             Console.ReadKey();
                         }
                         Console.Clear();
                         List<string> crudTables = new List<string>
                         {
                             "Create",
                             "List Of Tables",
                             "Exit"
                         };
                         
                         while (true)
                         {
                             Console.Clear();
                             int choiseTabAct = Services.Selector(crudTables);

                             switch (choiseTabAct)
                             {
                                 case 0:
                                     Console.WriteLine("(CREATE TABLE IF NOT EXISTS Users (Id SERIAL PRIMARY KEY, Name VARCHAR(100), Age INT);)\nYou can copy and edit sample above\nEnter query to create table:");
                                     string query = Console.ReadLine();
                                     Services.CreateTable(query);
                                     break;
                                 case 1:
                                     List<string> tablesList = Services.GetTableNames();
                                     if (tablesList.Count > 0)
                                     {
                                         tablesList.Add("Exit");
                                         int selectedTable = Services.Selector(tablesList);

                                         if (selectedIndex < tablesList.Count - 1)
                                         {
                                             Console.Clear();
                                             string choosenTable = tablesList[selectedTable];

                                             while (true)
                                             {
                                                 List<string> tableActions = new List<string>
                                                 {
                                                     "Delete",
                                                     "Delete(Cascade)",
                                                     "Scripts",
                                                     "Truncate",
                                                     "View/Edit data",
                                                     "Query tool",
                                                     "Exit"
                                                 };

                                                 int tableActionsChoise = Services.Selector(tableActions);

                                                 switch (tableActionsChoise)
                                                 {
                                                     case 0:
                                                         Console.WriteLine($"Are you sure you want to delete table {choosenTable}?(type: yes/no):");
                                                         string deleteAns = Console.ReadLine();
                                                         if (deleteAns.ToLower() == "yes")
                                                         {
                                                             Services.DeleteTable(choosenTable);
                                                         }
                                                         break;
                                                     case 1:
                                                         break;
                                                     case 2:
                                                         break;
                                                     case 3:
                                                         break;
                                                     case 4:
                                                         break;
                                                     case 5:
                                                         break;
                                                     case 6:
                                                         return;
                                                 }
                                             }

                                         }
                                             break;
                                     }
                                     else
                                     {
                                         Console.Clear();
                                         Console.WriteLine("There's no tables, press any button to returt to previous manu.");
                                         Console.ReadKey();
                                     }
                                     break;
                                 case 2:
                                     return;
                             }
                         }
                         
                     }
                     else
                     {
                         Console.WriteLine("No databases found. Please check the connection string info and try again");
                         Console.Write("Press any key to continue");
                         Console.ReadKey();
                         Console.Clear();
                     }
                     break;
                 case 1:
                     Console.WriteLine("Thank you for using our app!");
                     return;
             }
         }

    }
    
}