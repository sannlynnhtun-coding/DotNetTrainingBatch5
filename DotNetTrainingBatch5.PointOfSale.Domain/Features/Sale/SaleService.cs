using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Sale;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Features.Sale;

public class SaleService
{
   /* private readonly AppDbContext _db;

    public SaleService()
    {
        _db = new AppDbContext();
    }

    public async Task<Result<SaleResModel>> GetSaleByCode(string saleCode)
    {
        Result<SaleResModel> response = new Result<SaleResModel>();
        //TblSale? sale = await _db.TblSales.AsNoTracking()
        //    .Where(s => s.DeleteFlag == false && s.SaleCode == saleCode)
        //    .Select(sale => new ExtendedSale
        //    {
        //        SaleId = sale.SaleId,
        //        SaleCode = sale.SaleCode,
        //        SaleDate = sale.SaleDate,
        //        TotalSale = sale.TotalSale,
        //        PayAmount = sale.PayAmount,
        //        ChangeAmount = sale.ChangeAmount,
        //        SaleDetails = _db.TblSaleDetails.AsNoTracking()
        //        .Where(sd => sd.SaleCode == sale.SaleCode && sd.DeleteFlag == false)
        //        .Select(sd => new ExtendedSaleDetail
        //        {
        //            DetailId = sd.DetailId,
        //            DetailCode = sd.DetailCode,
        //            ProductCode = sd.ProductCode,
        //            ProductQuantity = sd.ProductQuantity,
        //            Total = sd.Total,
        //            SaleCode = sd.SaleCode,
        //            Products = new List<TblProduct> { sd.ProductCodeNavigation }
        //        }).ToList()
        //    })
        //    .FirstOrDefaultAsync();
        var sale = await _db.TblSales.AsNoTracking()
            .Include(s => s.TblSaleDetails)
            .ThenInclude(sd => sd.ProductCodeNavigation)
            .Where(s => s.DeleteFlag == false && s.SaleCode == saleCode)
            .FirstOrDefaultAsync();
        if (sale is null)
        {
            response = Result<SaleResModel>.NotFoundError("Sale Not Found!");
            goto Result;
        }

        SaleResModel model = new SaleResModel { Sale = (ExtendedSale)sale };
        response = Result<SaleResModel>.Success(model, "Sale Found!");

    Result:
        return response;
    }

    public async Task<Result<List<TblSale>>> GetSalesByMonth(int month, int year)
    {
        Result<List<TblSale>> response = new Result<List<TblSale>>();

        // Validate year
        if (year < 1900 || year > DateTime.Now.Year)
        {
            response = Result<List<TblSale>>.ValidationError("Year must be between 1900 and the current year.");
            goto Result;
        }

        // Validate month
        if (month < 1 || month > 12)
        {
            response = Result<List<TblSale>>.ValidationError("Month must be between 1 and 12.");
            goto Result;
        }
        DateTime startDate = new DateTime(year, month, 1);
        DateTime endDate;
        if (month == 12)
        {
            endDate = new DateTime(year + 1, 1, 1);
        }
        else
        {
            endDate = new DateTime(year, month + 1, 1);
        }

        var sales = await _db.TblSales.AsNoTracking()
            .Include(s => s.TblSaleDetails)
            .ThenInclude(sd => sd.ProductCodeNavigation)
            .Where(x => x.DeleteFlag == false && x.SaleDate < endDate && x.SaleDate >= startDate)
            .ToListAsync();

        if (sales is null)
        {
            response = Result<List<TblSale>>.NotFoundError("Sale Not Found!");
            goto Result;
        }

        response = Result<List<TblSale>>.Success(sales);

    Result:
        return response;
    }

    public async Task<Result<SaleResModel>> CreateSale()
    {
        Result<SaleResModel> response = new Result<SaleResModel>();
        TblSale? newSale = default;
        await _db.AddAsync(newSale);
        await _db.SaveChangesAsync();

        SaleResModel saleResModel = new SaleResModel
        {
            Sale = (ExtendedSale)newSale
        };

        response = Result<SaleResModel>.Success(saleResModel, "New Skeleton Sale!");

        return response;
    }

    public async Task<Result<SaleResModel>> RecalculateTotal(string saleCode)
    {
        Result<SaleResModel> response = new Result<SaleResModel>();

        var sale = await _db.TblSales.AsNoTracking().Where(s => s.DeleteFlag == false && s.SaleCode == saleCode).FirstOrDefaultAsync();
        if (sale is null)
        {
            response = Result<SaleResModel>.NotFoundError("Sale Not Found!");
            goto Result;
        }

        var list = await _db.TblSaleDetails
            .AsNoTracking()
            .Where(sd => sd.DeleteFlag == false && sd.SaleCode == saleCode)
            .ToListAsync();
        if (sale is null)
        {
            response = Result<SaleResModel>.NotFoundError("Sale Details are empty!");
            goto Result;
        }

        foreach (var item in list)
        {
            sale.TotalSale += item.Total;
        }

        _db.Entry(sale).State = EntityState.Modified;
        int result = await _db.SaveChangesAsync();

        if (result == 0)
        {
            response = Result<SaleResModel>.Error("Something went wrong, when recalculating total!");
            goto Result;
        }

        SaleResModel saleResModel = new SaleResModel
        {
            Sale = (ExtendedSale)sale,
        };
        response = Result<SaleResModel>.Success(saleResModel, "Sale Recalculated!");

    Result:
        return response;
    }

    public async Task<Result<SaleResModel>> DeleteSale(string saleCode)
    {
        Result<SaleResModel> response = new Result<SaleResModel>();

        var sale = await _db.TblSales.AsNoTracking().Where(s => s.DeleteFlag == false && s.SaleCode == saleCode).FirstOrDefaultAsync();
        if (sale is null)
        {
            response = Result<SaleResModel>.NotFoundError("Sale Not Found!");
            goto Result;
        }

        sale.DeleteFlag = true;
        _db.Entry(sale).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        SaleResModel saleResModel = new SaleResModel { Sale = (ExtendedSale)sale, };
        response = Result<SaleResModel>.Success(saleResModel, "Sale Deleted Successfully");

    Result:
        return response;
    }
*/
}
