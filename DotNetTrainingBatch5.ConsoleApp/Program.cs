﻿// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

//Console.WriteLine("Hello, World!");
//Console.ReadLine();

// md => markdown

// C# <=> Database

// ADO.NET
// Dapper (ORM)
// EFCore / Entity Framework (ORM)

// C# => sql query => 

// nuget 

// Ctrl + .

// max connection = 100
// 100 = 99

// 101



//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("dafadfs", "dasdfasssf", "dfasf");
//dapperExample.Edit(1);
//dapperExample.Edit(2);

//EFCoreExample eFCoreExample = new EFCoreExample();
////eFCoreExample.Read();
//eFCoreExample.Create("dafadfs", "dasdfasssf", "dfasf");

//string query = " [BlogAuthor] = @BlogAuthor, ";
//Console.WriteLine(query.Substring(0, query.Length - 2));

//DapperExample2 dapperExample2 = new DapperExample2();
//dapperExample2.Read();

var services = new ServiceCollection()
    .AddSingleton<AdoDotNetExample>()
    .BuildServiceProvider();

var adoDotNetExample = services.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

Console.ReadKey();