using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Features.Products
{
    public class ProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<Result<ResultProductResponseModel>> CreateProductAsync(int id, string productCode, string productName, decimal price)
        {
            var product = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);

            if (product is null)
            {
                return Result<ResultProductResponseModel>.SystemError("Failed to add the product");
            }

            var newProduct = new TblProduct
            {
                ProductCode = productCode,
                ProductName = productName,
                Price = price,
                InstockQuantity = 0,
                //DeleteFlag = false
            };

            _db.TblProducts.Add(newProduct);
            await _db.SaveChangesAsync();

            var item = new ResultProductResponseModel
            {
                Product = newProduct
            };
            var model = Result<ResultProductResponseModel>.Success(item, "Success.");

            return model;
        }

        public async Task CreateProductAsync(string productCode, string? productName, ProductResModel reqModel)
        {
            throw new NotImplementedException();
        }
    }
}
