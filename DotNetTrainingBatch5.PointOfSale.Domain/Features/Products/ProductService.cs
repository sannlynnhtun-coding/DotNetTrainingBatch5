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

        public async Task<TblProduct?> GetProductAsync(int id)
        {
            var item = await _db.TblProducts.FirstOrDefaultAsync(u => u.ProductId == id);
            return item;

        }

        public async Task<List<TblProduct>> GetProductsAsync()
        {
           var products =  await _db.TblProducts.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<Result<ResultProductResponseModel>> UpdateProductAsync(int id, string productCode, string productName, decimal price)
        {
            
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();

            
            var product = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);

            
            if (product is null)
            {
               
                model = Result<ResultProductResponseModel>.SystemError("Product not found.");
                return model;
            }

            
            product.ProductCode = productCode;
            product.ProductName = productName;
            product.Price = price;

            
            await _db.SaveChangesAsync();

            
            var responseModel = new ResultProductResponseModel
            {
                Product = new List<TblProduct?> { product }
            };

            
            model = Result<ResultProductResponseModel>.Success(responseModel, "Product updated successfully.");
            return model;
        }


        public async Task<Result<ResultProductResponseModel>> CreateProductAsync(string productCode, string productName, decimal price, int InstockQuantity)
        {
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();

            if (productCode.Length > 4)
            {
                model = Result<ResultProductResponseModel>.SystemError("ProductCode exceeds maximum length.");
                return model;
            }


            var newProduct = new TblProduct
            {
                ProductCode = productCode,
                ProductName = productName,
                Price = price,
                InstockQuantity = InstockQuantity,

            };

            await _db.TblProducts.AddAsync(newProduct);
            await _db.SaveChangesAsync();

            var item = new ResultProductResponseModel
            {
                Product = new List<TblProduct?> { newProduct }
            };
            model = Result<ResultProductResponseModel>.Success(item, "Success.");


            return model;
        }

        public async Task<TblProduct?> DeleteProductAsync(int id)
        {
            var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
            if(item is null)
            {
                return null;
            }

            _db.TblProducts.Remove(item);
            await _db.SaveChangesAsync();
            return item;

        }

        //public async Task<Result<ResultProductResponseModel>> CreateProductAsync(string productCode, string productName, decimal price)
        //{
        //    // Validate inputs
        //    if (string.IsNullOrWhiteSpace(productCode))
        //        return Result<ResultProductResponseModel>.SystemError("ProductCode cannot be empty.");
        //    if (productCode.Length > 4)
        //        return Result<ResultProductResponseModel>.SystemError("ProductCode exceeds the maximum allowed length of 4 characters.");
        //    if (string.IsNullOrWhiteSpace(productName))
        //        return Result<ResultProductResponseModel>.SystemError("ProductName cannot be empty.");
        //    if (price <= 0)
        //        return Result<ResultProductResponseModel>.SystemError("Price must be greater than zero.");

        //    try
        //    {
        //        // Create new product
        //        var newProduct = new TblProduct
        //        {
        //            ProductCode = productCode,
        //            ProductName = productName,
        //            Price = price,
        //            InstockQuantity = 0
        //        };

        //        _db.TblProducts.Add(newProduct);
        //        await _db.SaveChangesAsync();

        //        // Prepare response
        //        var response = new ResultProductResponseModel
        //        {
        //            Product = new List<TblProduct?> { newProduct }
        //        };
        //        return Result<ResultProductResponseModel>.Success(response, "Product created successfully.");
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Result<ResultProductResponseModel>.SystemError($"Concurrency error: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<ResultProductResponseModel>.SystemError($"Database error: {ex.Message}");
        //    }
        //}

    }
}
