using System;
using System.Data.SqlClient;

using Dapper;

using DataAccess_Dapper.Model;


namespace DataAccess_Dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=meubanco;User ID=user;Password=senha";

            

            using (var connection = new SqlConnection(connectionString))
            {
                ListCategories(connection);
                CreateCategory(connection); 
            }
        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id}");
            }

        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destina a serviços da amazon";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO [Category] 
                            VALUES(@Id,@Title,@Url,@Summary,@Order, @Description, @Featured)";

            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"{rows} linhas inseridas");
        }    
        
        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("b9809ce4-85f2-41e5-9cf3-dc4975f167a4"),
                title = "New title"
            });
        }
    }


}
