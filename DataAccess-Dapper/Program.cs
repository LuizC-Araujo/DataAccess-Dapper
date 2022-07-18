﻿using System;
using System.Data;
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
                //ListCategories(connection);
                //DeleteCategory(connection);
                //UpdateCategory(connection);
                //CreateCategory(connection); 
                //CreateManyCategory(connection);
                //ExecuteProcedure(connection);
                //ExecuteReadProcedure(connection);
                //ExecuteScalar(connection);
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

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE [Category] WHERE [id]=@id";
            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("b9809ce4-85f2-41e5-9cf3-dc4975f167a4")
            });
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destina a serviços da amazon";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Nova Categoria";
            category2.Url = "new-one";
            category2.Description = "Categoria destina a outros serviços";
            category2.Order = 9;
            category2.Summary = "nova categoria";
            category2.Featured = true;

            var insertSql = @"INSERT INTO [Category] 
                            VALUES(@Id,@Title,@Url,@Summary,@Order, @Description, @Featured)";

            var rows = connection.Execute(insertSql, new[]
            {
                new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            },
                new
            {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
            }
            });

            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "[procedure_name]";
            var pars = new { Id = "b9809ce4-85f2-41e5-9cf3-dc4975f167a4" };

            var affectedRows = connection.Execute(
                procedure, 
                pars, 
                commandType: CommandType.StoredProcedure);

            Console.WriteLine($"{affectedRows} linhas afetadas");
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[procedure_name]";
            var pars = new { Id = "b9809ce4-85f2-41e5-9cf3-dc4975f167a4" };

            var categorias = connection.Query(
                procedure,
                pars,
                commandType: CommandType.StoredProcedure);

            foreach(var categoria in categorias)
            {
                Console.WriteLine(categoria.Id);
            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destina a serviços da amazon";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO [Category] 
                            OUTPUT inserted.[id]
                            VALUES(NEWID(),@Title,@Url,@Summary,@Order, @Description, @Featured)";

            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            });

            Console.WriteLine($"A categoria inserida foi: {id}");
        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT FROM [viewName]";
            var categories = connection.Query(sql);
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id}");
            }
        }
       
    }


}
