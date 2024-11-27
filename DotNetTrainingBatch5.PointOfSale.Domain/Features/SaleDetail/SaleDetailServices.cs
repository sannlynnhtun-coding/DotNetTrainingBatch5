using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Sale;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.SaleDetail;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Features.SaleDetail;

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
    public async Task<SaleReqModel?> CreateShellSaleAsync() {
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
        
       return new SaleReqModel { 
       SaleCode = sale.SaleCode,
       PayAmount = sale.PayAmount
       };
        // possible error for salecode return 

    }
    // need to be in SALE services
   

    // assume in SALEDETAIL there is more than one product to sell and detail code for each bunch of product sell but the saleCode will be the same for each bunch of product sell (1,1)*,1
  
    public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailFromOneProductAsync(SaleProductReqModel product,SaleDetailReqModel saleReq) // for one product

    {
        // creating temporary shellcode
        //var saleReq = await CreateShellSale();
        if (saleReq is null) return Result<ResultSaleDetailResModel>.InvalidDataError("Error Data Commuting");


        //Adding a  of product to detail 
        TblSaleDetail Detail = new TblSaleDetail()
        {
            ProductCode = product.ProductCode,
            SaleCode = saleReq.SaleCode,
            Total = product.Price * product.SaleQuantity,// price need to be careful for wrong entry
            ProductQuantity = product.SaleQuantity



        };
        await _appDb.TblSaleDetails.AddAsync(Detail);
        int result = await _appDb.SaveChangesAsync();
        if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");

        //using the shell salecode to add data a sale

        var SaleItem =await _appDb.TblSales.AsNoTracking().Where(x=>x.SaleCode==Detail.SaleCode && !x.DeleteFlag).FirstOrDefaultAsync();
        if (SaleItem is null) return Result<ResultSaleDetailResModel>.SystemError("Error retriving Sale");

        SaleItem.TotalSale += Detail.Total;
        
        _appDb.Entry(SaleItem).State = EntityState.Modified;
       result = await _appDb.SaveChangesAsync ();
        if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error adding Saledetail to  Sale");
       




        ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = { Detail } };
        return Result<ResultSaleDetailResModel>.Success(resultList);



    }

    // write by taking normal perimeter

    public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailAsync(string saleCode,string productCode,int quantity,decimal Price) 
    {
        var detail = new TblSaleDetail
        {
            SaleCode = saleCode,
            ProductCode = productCode,
            ProductQuantity = quantity,
            Total = Price * quantity
        };

        await _appDb.TblSaleDetails.AddAsync(detail);
        int result = await _appDb.SaveChangesAsync();
        if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");

        var SaleItem = await _appDb.TblSales.AsNoTracking().Where(x => x.SaleCode == saleCode && !x.DeleteFlag).FirstOrDefaultAsync();
        if (SaleItem is null) return Result<ResultSaleDetailResModel>.SystemError("Error retriving Sale");

        SaleItem.TotalSale += detail.Total;

        _appDb.Entry(SaleItem).State = EntityState.Modified;
        result = await _appDb.SaveChangesAsync();
        if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error adding Saledetail to  Sale");


        ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = { detail } };
        return Result<ResultSaleDetailResModel>.Success(resultList);



    }

    // assume in SALEDETAIL there is more than one product to sell and detail code for each bunch of product sell but the saleCode will be the same for each bunch of product sell (1-1)*-1
    //to update each detail calling detail code is sufficient and after update also make an update in SALE**






    public async Task<Result<ResultSaleDetailResModel>?> UpdateSaleDetailAsync(string detailcode, SaleDetailReqModel model)
    {
        
        var detailItem = await _appDb.TblSaleDetails.AsNoTracking().Where(x => !x.DeleteFlag && x.DetailCode == detailcode).FirstOrDefaultAsync();
        if (detailItem is null) return Result<ResultSaleDetailResModel>.NotFoundError();

        decimal oldTotal = detailItem.Total;
        decimal pricePerUnit = detailItem.ProductQuantity > 0 ? detailItem.Total / detailItem.ProductQuantity : 0;

        //patch product code
        if (!string.IsNullOrEmpty(model.ProductCode))
        {
            detailItem.ProductCode = model.ProductCode;

            // Fetch price from TblProduct for recalculation
            var product = await _appDb.TblProducts.AsNoTracking().Where(x => x.ProductCode == model.ProductCode).FirstOrDefaultAsync();

            if (product is not null)
            {
                pricePerUnit = product.Price;
                detailItem.Total = pricePerUnit * detailItem.ProductQuantity;
            }
        }

        // patch product quatity
        if (model.ProductQuantity > 0)
        {
            detailItem.ProductQuantity = model.ProductQuantity;
            detailItem.Total = pricePerUnit * model.ProductQuantity;
        }

        // patch salecode 
        if (!string.IsNullOrEmpty(model.SaleCode) && model.SaleCode != detailItem.SaleCode)
        {
            var oldSale = await _appDb.TblSales.Where(x => x.SaleCode == detailItem.SaleCode).FirstOrDefaultAsync();

            if (oldSale is not null)
            {
                oldSale.TotalSale -= oldTotal;
                _appDb.Entry(oldSale).State = EntityState.Modified;
            }

            var newSale = await _appDb.TblSales.Where(x => x.SaleCode == model.SaleCode).FirstOrDefaultAsync();

            if (newSale is not null)
            {
                newSale.TotalSale += detailItem.Total;
                _appDb.Entry(newSale).State = EntityState.Modified;
            }

            detailItem.SaleCode = model.SaleCode;
        }
        else
        {
            // patch for productCode or quantity change
            var sale = await _appDb.TblSales.Where(x => x.SaleCode == detailItem.SaleCode).FirstOrDefaultAsync();

            if (sale is not null)
            {
                var diff = detailItem.Total - oldTotal;
                sale.TotalSale += diff;
                sale.ChangeAmount -=diff;
                _appDb.Entry(sale).State = EntityState.Modified;
            }
        }

       
        _appDb.Entry(detailItem).State = EntityState.Modified;
        int result = await _appDb.SaveChangesAsync();

        if (result == 0)
            return Result<ResultSaleDetailResModel>.SystemError("An error occurred while updating sale details.");

        return Result<ResultSaleDetailResModel>.Success(new ResultSaleDetailResModel{ 
                                    SaleDeails = new List<TblSaleDetail> { detailItem }}
                                    );
    }



    //function to change the Tbl_SAle total and the change done by taking the sale code 

    // to change product code just delete and Minus Sale.Total
    //when Deleted Chnage Sale.Total are effected columns


    public async Task<Result<ResultSaleDetailResModel>?> DeleteSaleDetailAsync(string detailCode) { 
    
        var detailItem =await _appDb.TblSaleDetails.AsNoTracking().Where(x=>x.DetailCode == detailCode&& x.DeleteFlag == false).FirstOrDefaultAsync();

        if (detailItem is null) return Result<ResultSaleDetailResModel>.NotFoundError();

        detailItem.DeleteFlag = true;
        _appDb.Entry(detailItem).State= EntityState.Modified;

        // get sale through detailItem.SaleCode
        var sale = await _appDb.TblSales.AsNoTracking().Where(x => x.SaleCode == detailItem.SaleCode) .FirstOrDefaultAsync();
        if (sale is  null) return Result<ResultSaleDetailResModel>.SystemError("Error retriving Sale");

        
            sale.TotalSale -= detailItem.Total;
            sale.ChangeAmount  =sale.PayAmount -sale.TotalSale;
            _appDb.Entry(sale).State = EntityState.Modified;
        
        //subtract from Tblsale.total and change +





        int result = await _appDb.SaveChangesAsync();
        if (result == 0)
            return Result<ResultSaleDetailResModel>.SystemError("An error occurred while deleting  sale details.");

        return Result<ResultSaleDetailResModel>.Success(new ResultSaleDetailResModel
        {
            SaleDeails = new List<TblSaleDetail> { detailItem }
        }
                                    );




    }

}



/*public async Task<string?> CreateSaleRecord(SaleReqModel sale, List<TblSaleDetail> detailList) // just some example
{

var saleItem = await _appDb.TblSales.AsNoTracking().Where(x => x.SaleCode == detailList[0].SaleCode).FirstOrDefaultAsync();
if (saleItem == null) return null;
foreach (var detail in detailList)
{
    if (saleItem.SaleCode == detail.SaleCode)
    {

        saleItem.TotalSale += detail.Total;


    }

}
saleItem.PayAmount = sale.PayAmount;
saleItem.ChangeAmount = saleItem.PayAmount - saleItem.PayAmount;
saleItem.SaleDate = DateTime.Now;

if (saleItem.ChangeAmount < 0) return null;

_appDb.Entry(saleItem).State = EntityState.Modified;
int result = await _appDb.SaveChangesAsync();
if (result == 0) return null;
return saleItem.TotalSale.ToString();
// possible error for salecode return 

}*/


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/*public async Task<Result<ResultSaleDetailResModel>?> CreateSaleDetailAsync(List<SaleProductReqModel> product) // for List of product

{
// creating temporary shellcode
var ShellSaleCode = await CreateShellSale();
if (ShellSaleCode is null) return Result<ResultSaleDetailResModel>.InvalidDataError("Error Data Commuting");


//Adding a list of product to detail 
List<TblSaleDetail> Detail = new List<TblSaleDetail>();


foreach (var item in product)
{
    Detail.Add(
        new TblSaleDetail
        {
            ProductCode = item.ProductCode,
            SaleCode = ShellSaleCode,
            Total = item.Price * item.SaleQuantity,// price need to be careful for wrong entry
            ProductQuantity = item.SaleQuantity



        }


        );

}

// possible action : take the saleDetail from this request to create a Sale by adding the amount of eact list to temporary shell Sale to make it official





await _appDb.TblSaleDetails.AddRangeAsync(Detail);
int result = await _appDb.SaveChangesAsync();
if (result == 0) return Result<ResultSaleDetailResModel>.SystemError("Error Saving Detail");




ResultSaleDetailResModel resultList = new ResultSaleDetailResModel() { SaleDeails = Detail };
return Result<ResultSaleDetailResModel>.Success(resultList);



}*/