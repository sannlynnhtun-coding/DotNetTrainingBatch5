using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace DotNetTrainingBatch5.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7212");
            var lst = await blogApi.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogTitle);
            }

            var item2 = await blogApi.GetBlog(1);
            try
            {
                var item3 = await blogApi.GetBlog(100);
            }
            catch (ApiException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found.");
                }
            }

            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "test",
                BlogAuthor = "test",
                BlogContent = "test",
            });
        }
    }
}
