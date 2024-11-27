using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetTrainingBatch5.ConsoleApp3;

public class HttpClientExample
{
    private readonly HttpClient _client;
    private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

    public HttpClientExample()
    {
        _client = new HttpClient();
    }

    public async Task Read()
    {
        var response = await _client.GetAsync(_postEndpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }

    public async Task Edit(int id)
    {
        var response = await _client.GetAsync($"{_postEndpoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data found.");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
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

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
        var response = await _client.PostAsync(_postEndpoint, content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
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

        var jsonRequest = JsonConvert.SerializeObject(requestModel);
        var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
        var response = await _client.PatchAsync($"{_postEndpoint}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task Delete(int id)
    {
        var response = await _client.DeleteAsync($"{_postEndpoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data found.");
            return;
        }

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonStr);
        }
    }
}

public class PostModel
{
    public int userId { get; set; }
    public int id { get; set; } // auto
    public string title { get; set; }
    public string body { get; set; }
}
