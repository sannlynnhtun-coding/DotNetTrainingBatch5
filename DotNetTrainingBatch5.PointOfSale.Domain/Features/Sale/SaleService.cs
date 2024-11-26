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
    private readonly AppDbContext _db;

    public SaleService()
    {
        _db = new AppDbContext();
    }

    public async Task<Result<SaleResModel>> GetSaleByCode(string saleCode)
    {
        Result<SaleResModel> response = new Result<SaleResModel>();
        var sale = await _db.TblSales.AsNoTracking().Where(s => s.DeleteFlag == false && s.SaleCode == saleCode).FirstOrDefaultAsync();
        if (sale is null)
        {
            response = Result<SaleResModel>.NotFoundError("Sale Not Found!");
            goto Result;
        }

        //TODO: also grab the associated sale details and products
        SaleResModel model = new SaleResModel { Sale = sale };
        response = Result<SaleResModel>.Success(model, "Sale Found!");

    Result:
        return response;
    }

    //public async Task<Result<List<SaleResModel>>> GetSalesByMonth

    public async Task<Result<SaleResModel>> CreateSale()
    {
        Result<SaleResModel> response = new Result<SaleResModel>();
        TblSale? newSale = default;
        await _db.AddAsync(newSale);
        await _db.SaveChangesAsync();

        SaleResModel saleResModel = new SaleResModel
        {
            Sale = newSale
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
            Sale = sale,
        };
        response = Result<SaleResModel>.Success(saleResModel, "Sale Recalculated!");

    Result:
        return response;
    }

}
