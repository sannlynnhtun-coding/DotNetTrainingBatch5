using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsAdoDotNetController : ControllerBase
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

    [HttpGet]
    public IActionResult GetBlogs()
    {
        List<BlogViewModel> lst = new List<BlogViewModel>();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader["BlogId"]);
            Console.WriteLine(reader["BlogTitle"]);
            Console.WriteLine(reader["BlogAuthor"]);
            Console.WriteLine(reader["BlogContent"]);
            //lst.Add(new BlogViewModel
            //{
            //    Id = Convert.ToInt32(reader["BlogId"]),
            //    Title = Convert.ToString(reader["BlogTitle"]),
            //    Author = Convert.ToString(reader["BlogAuthor"]),
            //    Content = Convert.ToString(reader["BlogContent"]),
            //    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
            //});
            var item = new BlogViewModel
            {
                Id = Convert.ToInt32(reader["BlogId"]),
                Title = Convert.ToString(reader["BlogTitle"]),
                Author = Convert.ToString(reader["BlogAuthor"]),
                Content = Convert.ToString(reader["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
            };
            lst.Add(item);
        }

        connection.Close();

        return Ok(lst);
    }

    //[HttpGet("{id}")]
    //public IActionResult GetBlog(int id)
    //{

    //}

    //[HttpPost]
    //public IActionResult CreateBlog(TblBlog blog)
    //{

    //}

    //[HttpPut("{id}")]
    //public IActionResult UpdateBlog(int id, TblBlog blog)
    //{

    //}

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogViewModel blog)
    {
        string conditions = "";
        if (!string.IsNullOrEmpty(blog.Title))
        {
            conditions += " [BlogTitle] = @BlogTitle, ";
        }
        if (!string.IsNullOrEmpty(blog.Author))
        {
            conditions += " [BlogAuthor] = @BlogAuthor, ";
        }
        if (!string.IsNullOrEmpty(blog.Content))
        {
            conditions += " [BlogContent] = @BlogContent, ";
        }

        if(conditions.Length == 0)
        {
            return BadRequest("Invalid Parameters!");
        }

        conditions = conditions.Substring(0, conditions.Length - 2);

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        if (!string.IsNullOrEmpty(blog.Title))
        {
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
        }
        if (!string.IsNullOrEmpty(blog.Author))
        {
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
        }
        if (!string.IsNullOrEmpty(blog.Content))
        {
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
        }

        int result = cmd.ExecuteNonQuery();

        connection.Close();

        return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
    }

    //[HttpDelete("{id}")]
    //public IActionResult DeleteBlog(int id)
    //{
    //}
}