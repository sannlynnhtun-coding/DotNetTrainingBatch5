using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static DotNetTrainingBatch5.PointOfSale.DataBase.Models.POSDataModel;

namespace DotNetTrainingBatch5.PointOfSale.DataBase.Models
{
    public class AddDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if(!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=.;Initial Catalog=PointOfSale;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<ProductDataModel> Products { get; set; }
        //public DbSet<ProductCategoryDataModel> ProductCategory { get; set; }
        //public DbSet<SaleDataModel> Sale { get; set; }
        //public DbSet<SaleDetailDataModel> SaleDetail { get; set; }

    }
}
