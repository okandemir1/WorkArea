using Microsoft.EntityFrameworkCore;
using WorkArea.Application.Filters;
using WorkArea.Domain.Entities;
using WorkArea.Persistence;
using WorkArea.Persistence.Repositories;

namespace WorkArea.Application.Services;

public class ArchiveTypeService(IRepository<ArchiveType> archiveTypeRepository)
{
    public DataTableViewModelResult<List<ArchiveType>> GetDataTableData(ArchiveTypeFilterModel filter)
    {
        var response = new DataTableViewModelResult<List<ArchiveType>>();
        response.IsSucceeded = true;

        var result = archiveTypeRepository.ListQueryableNoTracking.Where(x=>!x.IsDeleted);

        response.TotalCount = result.Count();
        response.RecordsFiltered = result.AddSearchFilters(filter).Count();
        response.Data = result.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();

        return response;
    }

    public async Task<DbOperationResult<int>> Create(ArchiveType archiveType)
    {
        var insert = await archiveTypeRepository.Insert(archiveType);
        return new DbOperationResult<int>(insert.IsSucceed, insert.Message, archiveType.Id);
    }
    
    public async Task<DbOperationResult<int>> Edit(ArchiveType archiveType)
    {
        var modelInDb = await archiveTypeRepository.ListQueryable
            .Where(x => x.Id == archiveType.Id && x.UserId == archiveType.UserId).FirstOrDefaultAsync();

        if (modelInDb == null)
            return new DbOperationResult<int>(false, "Güncellenecek veriye erişemedim");
        
        modelInDb.Name = archiveType.Name;
        modelInDb.UpdateDate = DateTime.Now;
        var update = await archiveTypeRepository.Update(modelInDb);
        return new DbOperationResult<int>(update.IsSucceed, update.Message, archiveType.Id);
    }
    
    public async Task<DbOperationResult> Delete(int id, int userId)
    {
        var modelInDb = await archiveTypeRepository.ListQueryable
            .Where(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();

        if (modelInDb == null)
            return new DbOperationResult(false, "Silinecek veriye erişemedim");

        modelInDb.IsDeleted = true;
        modelInDb.UpdateDate = DateTime.Now;
        var update = await archiveTypeRepository.Update(modelInDb);
        return update;
    }
    
    public async Task<ArchiveType?> GetArchiveTypeById(int id, int userId)
    {
        var data = await archiveTypeRepository.ListQueryable
            .Where(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();

        return data;
    }
}