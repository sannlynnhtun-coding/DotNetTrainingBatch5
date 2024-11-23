using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);

    return Results.Ok(result.Tbl_Bird);
})
.WithName("GetBirds")
.WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);

    if (item is null) return Results.BadRequest("No data found.");

    return Results.Ok(item);
})
.WithName("GetBird")
.WithOpenApi();

app.MapPost("/birds", (BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    requestModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
    result.Tbl_Bird.Add(requestModel);

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(requestModel);
})
.WithName("CreateBird")
.WithOpenApi();

app.MapPut("/birds/{id}", (int id, BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);

    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (item is null) return Results.BadRequest("No Data Found");

    var index = result.Tbl_Bird.FindIndex(x => x.Id == id);
    if (index != -1)
    {
        //result.Tbl_Bird[index] = requestModel;
        result.Tbl_Bird[index] = new BirdModel
        {
            Id = id,
            BirdEnglishName = requestModel.BirdEnglishName,
            BirdMyanmarName = requestModel.BirdMyanmarName,
            Description = requestModel.Description,
            ImagePath = requestModel.ImagePath
        };
    }
    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(result.Tbl_Bird[index]);

})
.WithName("UpdateBird")
.WithOpenApi();

app.MapDelete("birds/{id}", (int id) =>
{
    var folderPath = "Data/birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);

    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (item is null) return Results.BadRequest("No Data Found");

    result.Tbl_Bird.Remove(item);

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);

})
    .WithName("DeleteBird")
    .WithOpenApi();



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class BirdResponseModel
{
    public List<BirdModel> Tbl_Bird { get; set; }
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}

