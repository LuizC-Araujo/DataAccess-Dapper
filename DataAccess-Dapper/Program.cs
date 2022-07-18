using System;
using System.Data.SqlClient;

using Dapper;

using DataAccess_Dapper.Model;


namespace DataAccess_Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=meubanco;User ID=user;Password=senha";

            using (var connection = new SqlConnection(connectionString))
            {
                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
                foreach (var category in categories)
                {
                    Console.WriteLine($"{category.Id}");
                }
            
            }
        }
    }
}
