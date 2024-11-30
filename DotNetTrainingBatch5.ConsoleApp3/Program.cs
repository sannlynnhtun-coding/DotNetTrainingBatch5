// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp3;

Console.WriteLine("Hello, World!");

// get
// post
// put
// patch
// delete

// resource
// endpoint

//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Read();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(101);

//await httpClientExample.Create("test title", "test body", 1);
//await httpClientExample.Update(1, "test title", "test body", 10);

Console.Write("waiting for api...");
Console.ReadLine();

RefitExample refitExample = new RefitExample();
await refitExample.Run();


Console.ReadLine();