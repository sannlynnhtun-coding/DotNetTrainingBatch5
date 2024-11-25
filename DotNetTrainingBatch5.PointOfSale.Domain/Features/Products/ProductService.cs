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
        private TblProduct newProduct;

        public ProductService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<Result<ResultProductResponseModel>> GetAllProductAsync(int id, TblProduct product)
        {
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();


            var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);

            if (item is null)
            {
                model = Result<ResultProductResponseModel>.SystemError("No Data Found");
                goto Result;
            }

            //var newProduct = new TblProduct
            //{
            //    ProductCode = productCode,
            //    ProductName = productName,
            //    Price = price,
            //    InstockQuantity = 0,
            //    //DeleteFlag = false
            //};

            _db.TblProducts.Add(item);
            await _db.SaveChangesAsync();

            var getProduct = new ResultProductResponseModel
            {
                Product = newProduct
            };
            model = Result<ResultProductResponseModel>.Success(getProduct, "Success.");

        Result:
            return model;
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
                Product = product
            };

            
            model = Result<ResultProductResponseModel>.Success(responseModel, "Product updated successfully.");
            return model;
        }

        public async Task<Result<ResultProductResponseModel>> CreateProductAsync( string productCode, string productName, decimal price)
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
                InstockQuantity = 0,
                //DeleteFlag = false
            };

            _db.TblProducts.Add(newProduct);
            await _db.SaveChangesAsync();

            var item = new ResultProductResponseModel
            {
                Product = newProduct
            };
            model = Result<ResultProductResponseModel>.Success(item, "Success.");

        
            return model;
        }
    }
}
