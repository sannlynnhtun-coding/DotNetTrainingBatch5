using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.SaleDetail;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Features.SaleDetail
{
    public class SaleDetailServices
    {
        private readonly AppDbContext _appDb = new AppDbContext();

       public async Task<Result<ResultSaleDetailResModel>?> GetSaleDetailAsync() {
             
        var itemList = await _appDb.TblSaleDetails.Where(x=>x.DeleteFlag == false).ToListAsync();
            
            if (itemList.Count == 0 || itemList is null) return Result<ResultSaleDetailResModel>.NotFoundError();

            ResultSaleDetailResModel result = new ResultSaleDetailResModel() { SaleDeails = itemList};
            return Result<ResultSaleDetailResModel>.Success(result);



        }

        public async Task<Result<ResultSaleDetailResModel>?> GetSaleDetailByIdAsync(int id)
        {

            var itemList = await _appDb.TblSaleDetails.Where(x => x.DeleteFlag == false && (x.DetailId == id)).ToListAsync();
            if (itemList.Count == 0 || itemList is null) return Result<ResultSaleDetailResModel>.NotFoundError();

            ResultSaleDetailResModel result = new ResultSaleDetailResModel() { SaleDeails = itemList };
            return Result<ResultSaleDetailResModel>.Success(result);



        }

        public async Task<Result<ResultSaleDetailResModel>?> GetSaleDetailByCodeAsync(string code)
        {

            var itemList = await _appDb.TblSaleDetails.Where(x => x.DeleteFlag == false &&( x.DetailCode == code || x.ProductCode == code || x.SaleCode == code)).ToListAsync();
            if (itemList.Count == 0 || itemList is null) return Result<ResultSaleDetailResModel>.NotFoundError();

            ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = itemList };
            return Result<ResultSaleDetailResModel>.Success(resultList);



        }

        public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailAsync(ProductReqModel product, TblSale sale ) // need to change to SalereqModel
        {
            var detail = new TblSaleDetail { 
                SaleCode = sale.SaleCode,
                ProductCode = product.ProductCode,
                ProductQuantity = Convert.ToInt32( sale.TotalSale/product.Price),
                Total = sale.TotalSale
            };

            await _appDb.TblSaleDetails.AddAsync(detail);
           int result =  await _appDb.SaveChangesAsync();
            if(result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");

           


            ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = { detail } };
            return Result<ResultSaleDetailResModel>.Success(resultList);



        }
        // assume in SALEDETAIL there is more than one product to sell and detail code for each bunch of product sell but the saleCode will be the same for each bunch of product sell (1-1)*-1
        /* public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailAsync(List<ProductReqModel> product) // need to change to SalereqModel
         {
             TblSale sale = new TblSale();

             List<TblSaleDetail> newDetail = new List<TblSaleDetail>();
             newDetail[0] = new TblSaleDetail()
             {
                 SaleCode = sale.SaleCode,
                 ProductCode = product[0].ProductCode,
                 ProductQuantity = product[0].InstockQuantity, // need to change the req model of product to 
                 Total = product[0].Price* newDetail[0].ProductQuantity // checking needed
             };

             foreach (var item in product) { 


             }


             var detail = new TblSaleDetail
             {
                 SaleCode = sale.SaleCode,
                 ProductCode = product.ProductCode,
                 ProductQuantity = Convert.ToInt32(sale.TotalSale / product.Price),
                 Total = sale.TotalSale
             };

             await _appDb.TblSaleDetails.AddAsync(detail);
             int result = await _appDb.SaveChangesAsync();
             if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");




             ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = { detail } };
             return Result<ResultSaleDetailResModel>.Success(resultList);



         }
 */

        // write by taking normal perimeter

        public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailAsync(string saleCode,string productCode,int quantity,decimal amount) 
        {
            var detail = new TblSaleDetail
            {
                SaleCode = saleCode,
                ProductCode = productCode,
                ProductQuantity = quantity,
                Total = amount
            };

            await _appDb.TblSaleDetails.AddAsync(detail);
            int result = await _appDb.SaveChangesAsync();
            if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");




            ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = { detail } };
            return Result<ResultSaleDetailResModel>.Success(resultList);



        }
        // assume in SALEDETAIL there is more than one product to sell and detail code for each bunch of product sell but the saleCode will be the same for each bunch of product sell (1-1)*-1
        //to update each detail calling detail code is sufficient and after update also make an update in SALE**
        /*public async Task<Result<ResultSaleDetailResModel>?> UpdateSaleDetailAsync(int id,SaleDetailReqModel model) { 
        
            var item =await _appDb.TblSaleDetails.Where(x=>x.DeleteFlag==false && x.DetailId==id).FirstOrDefaultAsync();
            if(item == null) return Result<ResultSaleDetailResModel>.NotFoundError();


            var detail = new TblSaleDetail
            {
                SaleCode = model.SaleCode,
                ProductCode = model.ProductCode,
                ProductQuantity = Convert.ToInt32(sale.TotalSale / product.Price),
                Total = sale.TotalSale
            };

        }*/

    }
}
