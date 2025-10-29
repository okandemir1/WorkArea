using Microsoft.AspNetCore.Mvc;
using WorkArea.App.WebUI.Authorize;
using WorkArea.App.WebUI.Helpers;
using WorkArea.Application.Filters;
using WorkArea.Application.Services;
using WorkArea.Application.Validation;
using WorkArea.Domain.Entities;

namespace WorkArea.App.WebUI.Controllers;

[Authorize]
public class ArchiveTypeController(ArchiveTypeService archiveTypeService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult GetAllData(DataTableParameters dataTableParameters)
    {
        dataTableParameters.UserId = User.GetUserId();
        var response = archiveTypeService.GetDataTableData(new ArchiveTypeFilterModel(dataTableParameters));
        return Json(
            new
            {
                draw = dataTableParameters.Draw,
                recordsFiltered = response.RecordsFiltered,
                recordsTotal = response.TotalCount,
                data = response.Data
            });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new ArchiveType());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ArchiveType model)
    {
        var errors = new List<string>();
        var validCheck = new ArchiveTypeValidation().Validate(model);
        if (!validCheck.IsValid)
        {
            validCheck.Errors.ForEach(x => errors.Add(x.ErrorMessage));
            return Json(new { isSucceed = false, message = "Eksik veya hatalı veri girişi", title = "Hay Aksi", errors });
        }

        model.CreateDate = DateTime.Now;
        model.UpdateDate = DateTime.Now;
        model.UserId = User.GetUserId();
        model.IsDeleted = false;
        var result = await archiveTypeService.Create(model);
        return Json(new { isSucceed = result.IsSucceed, 
            message = result.Message, 
            title = result.IsSucceed ? "Başarılı" : "Hay Aksi", 
            errors, 
            id = result.Instance 
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var data = await archiveTypeService.GetArchiveTypeById(id, User.GetUserId());
        if(data == null)
            return NotFound();
        
        return View(data);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(ArchiveType model)
    {
        var errors = new List<string>();
        var validCheck = new ArchiveTypeValidation().Validate(model);
        if (!validCheck.IsValid)
        {
            validCheck.Errors.ForEach(x => errors.Add(x.ErrorMessage));
            return Json(new { isSucceed = false, message = "Eksik veya hatalı veri girişi", title = "Hay Aksi", errors });
        }

        model.UserId = User.GetUserId();
        var result = await archiveTypeService.Edit(model);
        return Json(new { isSucceed = result.IsSucceed, 
            message = result.Message, 
            title = result.IsSucceed ? "Başarılı" : "Hay Aksi", 
            errors,
            id = result.Instance
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await archiveTypeService.Delete(id, User.GetUserId());
        return Json(new { isSucceed = data.IsSucceed,
            message = data.IsSucceed ? "Veri Silindi" : data.Message});
    }
}