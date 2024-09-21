using DotNetTrainingBatch5.Database.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var lst = db.TblBlogs.ToList();