using Dapper;
using DotNetTrainingBatch5.ConsoleApp.Models;

namespace DotNetTrainingBatch5.ConsoleApp;

public class DapperExample
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";

    public void Read()
    {
        //using(IDbConnection db = new SqlConnection(_connectionString))
        //{
        //    string query = "select * from tbl_blog where DeleteFlag = 0;";
        //    var lst = db.Query(query).ToList();
        //    foreach (var item in lst)
        //    {
        //        Console.WriteLine(item.BlogId);
        //        Console.WriteLine(item.BlogTitle);
        //        Console.WriteLine(item.BlogAuthor);
        //        Console.WriteLine(item.BlogContent);
        //    }
        //}

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = db.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        // DTO =>  Data Transfer Object
    }

    public void Create(string title, string author, string content)
    {
        string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            int result = db.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }
    }

    public void Edit(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId;";
            var item = db.Query<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id
            }).FirstOrDefault();

            //if (item == null)
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
    }
}