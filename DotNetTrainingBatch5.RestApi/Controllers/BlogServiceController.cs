using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.Domain.Features.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.RestApi.Controllers
{
    // Presentation Layer
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogService _service;

        public BlogServiceController()
        {
            _service = new BlogService();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _service.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _service.GetBlog(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var model = _service.CreateBlog(blog);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _service.UpdateBlog(id, blog);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _service.PatchBlog(id, blog);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _service.DeleteBlog(id);
            if (item is null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
