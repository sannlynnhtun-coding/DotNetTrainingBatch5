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

       public async Task<SaleDetailResModel?> getSaleDetail() { 
        
        var itemList = await _appDb.TblSaleDetails.Where(x=>x.DeleteFlag == false).ToListAsync();
            if (itemList.Count == 0 || itemList is null) return null;
            return new SaleDetailResModel { SaleDeails = itemList };


        
        }

        public async Task<SaleDetailResModel?> getSaleDetailById(int id)
        {

            var itemList = await _appDb.TblSaleDetails.Where(x => x.DeleteFlag == false && (x.DetailId == id)).ToListAsync();
            if (itemList.Count == 0 || itemList is null) return null;
            return new SaleDetailResModel { SaleDeails = itemList };



        }

        public async Task<SaleDetailResModel?> getSaleDetailByCode(string code)
        {

            var itemList = await _appDb.TblSaleDetails.Where(x => x.DeleteFlag == false &&( x.DetailCode == code || x.ProductCode == code || x.SaleCode == code)).ToListAsync();
            if (itemList.Count == 0 || itemList is null) return null;
            return new SaleDetailResModel { SaleDeails = itemList };



        }

        //public async Task<SaleDetailResModel?> createSaleDetail(ProductReqModel product,SaleDetail) { 
        
            
        
        //}


    }
}
