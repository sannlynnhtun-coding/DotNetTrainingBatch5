using DotNetTrainingBatch5.PointOfSale.DataBase.Models;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.Product;
using DotNetTrainingBatch5.PointOfSale.Domain.Models.ProductCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.PointOfSale.Domain.Features.ProductCategory;

public class ProductCategoryService
{
   /* private readonly AppDbContext _db;

    public ProductCategoryService()
    {
        _db = new AppDbContext();
    }

    public async Task<Result<ResultCategoryResModel>> CreateCategory(string categoryName)
    {
        Result<ResultCategoryResModel> response = new Result<ResultCategoryResModel>();
        if (string.IsNullOrEmpty(categoryName) || categoryName.Length < 5)
        {
            response = Result<ResultCategoryResModel>.ValidationError("Category Name can't be less than 4 characters!");
            goto Result;
        }

        TblCategory newCategory = new TblCategory
        {
            CategoryName = categoryName,
        };

        await _db.AddAsync(newCategory);
        int result = await _db.SaveChangesAsync();

        if (result == 0)
        {
            response = Result<ResultCategoryResModel>.Error("Category Not Created!");
            goto Result;
        }

        ResultCategoryResModel resultProductResponseModel = new ResultCategoryResModel { Category = newCategory };
        response = Result<ResultCategoryResModel>.Success(resultProductResponseModel, "Category Created!");

    Result:
        return response;
    }

    public async Task<Result<List<TblCategory>>> GetCategories()
    {
        Result<List<TblCategory>> response = new Result<List<TblCategory>>();

        var lst = await _db.TblCategories.AsNoTracking().Where(c => c.DeleteFlag == false).ToListAsync();

        response = Result<List<TblCategory>>.Success(lst);

        return response;
    }

    public async Task<Result<ResultCategoryResModel>> GetCategoryByCode(string categoryCode)
    {
        Result<ResultCategoryResModel> response = new Result<ResultCategoryResModel>();

        var category = await _db.TblCategories.AsNoTracking().Where(c => c.CategoryCode == categoryCode && c.DeleteFlag == false).FirstOrDefaultAsync();
        if (category is null)
        {
            response = Result<ResultCategoryResModel>.NotFoundError("Category Not Found!");
            goto Result;
        }

        ResultCategoryResModel model = new ResultCategoryResModel { Category = category };
        response = Result<ResultCategoryResModel>.Success(model);

    Result:
        return response;
    }

    public async Task<Result<ResultCategoryResModel>> UpdateCategory(string categoryCode, string categoryName)
    {
        Result<ResultCategoryResModel> response = new Result<ResultCategoryResModel>();

        var getCategory = await GetCategoryByCode(categoryCode);
        if (getCategory.IsError) goto Result;

        var category = getCategory.Data.Category;
        category.CategoryName = categoryName;

        _db.Entry(category).State = EntityState.Modified;
        int result = await _db.SaveChangesAsync();

        if (result == 0)
        {
            response = Result<ResultCategoryResModel>.Error("Update Failed!");
            goto Result;
        }

        ResultCategoryResModel model = new ResultCategoryResModel { Category = category };
        response = Result<ResultCategoryResModel>.Success(model);

    Result:
        return response;
    }

    public async Task<Result<ResultCategoryResModel>> DeleteCategory(string categoryCode)
    {
        Result<ResultCategoryResModel> response = new Result<ResultCategoryResModel>();

        var getCategory = await GetCategoryByCode(categoryCode);
        if (getCategory.IsError) goto Result;

        var category = getCategory.Data.Category;
        category.DeleteFlag = false;

        _db.Entry(category).State = EntityState.Modified;
        int result = await _db.SaveChangesAsync();

        if (result == 0)
        {
            response = Result<ResultCategoryResModel>.Error("Delete Failed!");
            goto Result;
        }

        ResultCategoryResModel model = new ResultCategoryResModel { Category = category };
        response = Result<ResultCategoryResModel>.Success(model);

    Result:
        return response;
    }*/
}
