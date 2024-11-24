using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetTrainingBatch5.ConsoleApp3
{
    public class RestClientExample
    {
        private readonly RestClient _client;
        private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            _client = new RestClient();
        }

        public async Task Read()
        {
            RestRequest request = new RestRequest(_postEndpoint, Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Create(string title, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                body = body,
                title = title,
                userId = userId
            }; // C# object | .NET object

            RestRequest request = new RestRequest(_postEndpoint, Method.Post);
            request.AddJsonBody(requestModel);

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Update(int id, string title, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                id = id,
                body = body,
                title = title,
                userId = userId
            }; // C# object | .NET object

            RestRequest request = new RestRequest(_postEndpoint, Method.Patch);
            request.AddJsonBody(requestModel);

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Delete(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Delete);

            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
    }
}
