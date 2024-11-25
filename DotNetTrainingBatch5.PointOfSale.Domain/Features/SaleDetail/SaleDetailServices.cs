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

       

        //function to be edit for sale AMM
        public async Task<string?> CreateShellSale() {
            TblSale sale = new TblSale()
            {
                PayAmount = 0,
                SaleDate = DateTime.Now,
                ChangeAmount = 0,
                TotalSale = 0


            };

            await _appDb.TblSales.AddAsync(sale);
            int result = await _appDb.SaveChangesAsync();
            if (result == 0) return null;
           return sale.SaleCode;
            // possible error for salecode return 

        }

        // assume in SALEDETAIL there is more than one product to sell and detail code for each bunch of product sell but the saleCode will be the same for each bunch of product sell (1-1)*-1
        public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailAsync(List<SaleProductReqModel> product) // need to change to SalereqModel
         
        {
            // creating temporary shellcode
            var ShellSaleCode = await CreateShellSale();
            if(ShellSaleCode is null) return Result<ResultSaleDetailResModel>.InvalidDataError("Error Data Commuting");
           

            //Adding a list of product to detail 
            List<TblSaleDetail> Detail = new List<TblSaleDetail>();
             

             foreach (var item in product) {
                Detail.Add(
                    new TblSaleDetail {
                    ProductCode = item.ProductCode,
                    SaleCode = ShellSaleCode,
                    Total  =  item.Price * item.SaleQuantity,// price need to be careful for wrong entry
                    ProductQuantity = item.SaleQuantity
                    
                    
                    
                    }
                    
                    
                    );

             }

             // possible action : take the saleDetail from this request to create a Sale by adding the amount of eact list to temporary shell Sale to make it official



             

             await _appDb.TblSaleDetails.AddRangeAsync(Detail);
             int result = await _appDb.SaveChangesAsync();
             if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");




             ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = Detail  };
             return Result<ResultSaleDetailResModel>.Success(resultList);



         }
 

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
       public async Task<Result<ResultSaleDetailResModel>?> UpdateSaleDetailAsync(string detailcode,SaleDetailReqModel model) { 
        
            var detailItem =await _appDb.TblSaleDetails.AsNoTracking().Where(x=>x.DeleteFlag==false && x.DetailCode== detailcode).FirstOrDefaultAsync();
            if(detailItem is null) return Result<ResultSaleDetailResModel>.NotFoundError();

            decimal price = detailItem.Total/ detailItem.ProductQuantity;

            // if for patch
            detailItem.ProductQuantity = model.ProductQuantity;
            // if for patch
            detailItem.Total = price*model.ProductQuantity;
            //if for patch
            detailItem.SaleCode = model.SaleCode; // possile to change total in two Sale Tbl (minus from detailItem, add to model)

            //function to change the Tbl_SAle total and the change done by taking the sale code 

           // to change product code just delete and Minus Sale.Total
           //when Deleted Chnage Sale.Total and effected columns

            _appDb.Entry(detailItem).State = EntityState.Modified;
            int result = await _appDb.SaveChangesAsync();

            if (result is 0) { return Result<ResultSaleDetailResModel>.SystemError("Some Code Error Occur"); }

             
            return Result<ResultSaleDetailResModel>.Success(new ResultSaleDetailResModel { 
            SaleDeails= { detailItem }
            
            });


        }

    }
}
