using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace DotNetTrainingBatch5.RestApi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurmaProjectIdeaController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly RestClient _restClient;
        private readonly ISnakeApi _snakeApi;

        public BurmaProjectIdeaController(HttpClient httpClient, RestClient restClient, ISnakeApi snakeApi)
        {
            _httpClient = httpClient;
            _restClient = restClient;
            _snakeApi = snakeApi;
        }

        [HttpGet("birds")]
        public async Task<IActionResult> BirdsAsync([FromServices] HttpClient httpClient)
        {
            var response = await _httpClient.GetAsync("birds");
            string str = await response.Content.ReadAsStringAsync();
            return Ok(str);
        }

        [HttpGet("pick-a-pile")]
        public async Task<IActionResult> PickAPileAsync()
        {
            RestRequest request = new RestRequest("pick-a-pile", Method.Get);
            var response = await _restClient.GetAsync(request);
            return Ok(response.Content);
        }

        [HttpGet("snakes")]
        public async Task<IActionResult> Snakes()
        {
            var response = await _snakeApi.GetSnakes();
            return Ok(response);
        }
    }
}
