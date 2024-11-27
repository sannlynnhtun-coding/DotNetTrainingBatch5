using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Features.Products
{
    public class ProductService
    {
        private readonly AppDbContext _db;
      

        public ProductService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<Result<ResultProductResponseModel>> GetProductAsync(int id)
        {
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();

          
            var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == id);

         
            if (product is null)
            {
                model = Result<ResultProductResponseModel>.SystemError("Product not found.");
                goto Result;
            }

          
            var responseModel = new ResultProductResponseModel
            {
                Product = product
            };

          
            model = Result<ResultProductResponseModel>.Success(responseModel, "Product retrieved successfully.");

        Result:
            return model;

        }
        
        public async Task<Result<List<ResultProductResponseModel>>> GetProductsAsync()
        {
           
            Result<List<ResultProductResponseModel>> model = new Result<List<ResultProductResponseModel>>();

          
            var products = await _db.TblProducts.AsNoTracking().ToListAsync();

       
            if (products is null)
            {
                model = Result<List<ResultProductResponseModel>>.SystemError("No products found.");
                return model;
            }

          
            var responseModels = products.Select(product => new ResultProductResponseModel
            {
                Product = product
            }).ToList();

            
            model = Result<List<ResultProductResponseModel>>.Success(responseModels, "Products retrieved successfully.");

            return model;
        }


        public async Task<Result<ResultProductResponseModel>> UpdateProductAsync(int id, string productCode, string productName, decimal price ,int instockQuantity)
        {
            
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();

            
            var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);

            
            if (product is null)
            {
               
                model = Result<ResultProductResponseModel>.SystemError("Product not found.");
                return model;
            }


            var newProduct = new TblProduct
            {
                ProductCode = productCode,
                ProductName = productName,
                Price = price,
                InstockQuantity = instockQuantity,

            };

            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            
            var responseModel = new ResultProductResponseModel
            {
                Product = newProduct
            };

            
            model = Result<ResultProductResponseModel>.Success(responseModel, "Product updated successfully.");
            return model;
        }


        public async Task<Result<ResultProductResponseModel>> CreateProductAsync(string productCode, string productName, decimal price, int InstockQuantity)
        {
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();


            var existingProductCode = _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == x.ProductCode);

            if(existingProductCode is null)
            {
               model = Result<ResultProductResponseModel>.SystemError("Product with the same code already exists.");
                goto Result;
             }

            if (productCode.Length > 4)
            {
                model = Result<ResultProductResponseModel>.ValidationError("productCode must be 4 character");
                goto Result;
            }
          

            var newProduct = new TblProduct
            {
                ProductCode = productCode,
                ProductName = productName,
                Price = price,
                InstockQuantity = InstockQuantity,

            };

            await _db.TblProducts.AddAsync(newProduct);
            //_db.Entry(newProduct).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            var item = new ResultProductResponseModel
            {
                Product =  newProduct
            };
            model = Result<ResultProductResponseModel>.Success(item, "Success.");

            Result:
            return model;
        }

        public async Task<Result<ResultProductResponseModel>> DeleteProductAsync(int id)
        {
            Result<ResultProductResponseModel> model = new Result<ResultProductResponseModel>();


            var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == id);


            if (product is null)
            {
                model = Result<ResultProductResponseModel>.SystemError("Product not found.");
                goto Result;
            }


            var responseModel = new ResultProductResponseModel
            {
                Product = product
            };


            model = Result<ResultProductResponseModel>.Success(responseModel, "Product retrieved successfully.");

        Result:
            return model;
        }


    }
}
